<%@ WebHandler Language="C#" Class="RoxyFilemanHandler" Debug="true" %>
/*
  RoxyFileman - web based file manager. Ready to use with CKEditor, TinyMCE. 
  Can be easily integrated with any other WYSIWYG editor or CMS.

  Copyright (C) 2013, RoxyFileman.com - Lyubomir Arsov. All rights reserved.
  For licensing, see LICENSE.txt or http://RoxyFileman.com/license

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.

  Contact: Lyubomir Arsov, liubo (at) web-lobby.com
*/

using System;
using System.Reflection;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.IO.Compression;

public class RoxyFilemanHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    Dictionary<string, string> _settings = null;
    Dictionary<string, string> _lang = null;
    HttpResponse _r = null;
    HttpContext _context = null;
    string confFile = "../conf.json";
    public void ProcessRequest (HttpContext context) {
        _context = context;
        _r = context.Response;
        string action = "DIRLIST";
        try{
            if (_context.Request["a"] != null)
                action = (string)_context.Request["a"];
            
            VerifyAction(action);
            switch (action.ToUpper())
            {
                case "DIRLIST":
                    ListDirTree(_context.Request["type"]);
                    break;
                case "FILESLIST":
                    ListFiles(_context.Request["d"], _context.Request["type"]);
                    break;
                case "COPYDIR":
                    CopyDir(_context.Request["d"], _context.Request["n"]);
                    break;
                case "COPYFILE":
                    CopyFile(_context.Request["f"], _context.Request["n"]);
                    break;
                case "CREATEDIR":
                    CreateDir(_context.Request["d"], _context.Request["n"]);
                    break;
                case "DELETEDIR":
                    DeleteDir(_context.Request["d"]);
                    break;
                case "DELETEFILE":
                    DeleteFile(_context.Request["f"]);
                    break;
                case "DOWNLOAD":
                    DownloadFile(_context.Request["f"]);
                    break;
                //case "DOWNLOADDIR":
                //    DownloadDir(_context.Request["d"]);
                //    break;
                case "MOVEDIR":
                    MoveDir(_context.Request["d"], _context.Request["n"]);
                    break;
                case "MOVEFILE":
                    MoveFile(_context.Request["f"], _context.Request["n"]);
                    break;
                case "RENAMEDIR":
                    RenameDir(_context.Request["d"], _context.Request["n"]);
                    break;
                case "RENAMEFILE":
                    RenameFile(_context.Request["f"], _context.Request["n"]);
                    break;
                case "GENERATETHUMB":
                    int w = 140, h = 0;
                    int.TryParse(_context.Request["width"].Replace("px", ""), out w);
                    int.TryParse(_context.Request["height"].Replace("px", ""), out h);
                    ShowThumbnail(_context.Request["f"], w, h);
                    break;
                case "UPLOAD":
                    Upload(_context.Request["d"]);
                    break;
                default:
                    _r.Write(GetErrorRes("This action is not implemented."));
                    break;
            }
        
        }
        catch(Exception ex){
            if (action == "UPLOAD" && !IsAjaxUpload())
            {
                _r.Write("<script>");
                _r.Write("parent.fileUploaded(" + GetErrorRes(LangRes("E_UploadNoFiles")) + ");");
                _r.Write("</script>");
            }
            else{
                _r.Write(GetErrorRes(ex.Message));
            }
        }
        
    }

    private void isAuthenticated() 
    {
        if (!ControleLogin.AdmLogado())
            throw new Exception("Usuário não logado.");
    }
    
    
    private string FixPath(string path)
    {
        isAuthenticated();
        if (!path.StartsWith("~")){
            if (!path.StartsWith("/"))
                path = "/" + path;
            path = "~" + path;
        }
        return _context.Server.MapPath(path);
    }
    private string GetLangFile(){
        isAuthenticated();
        string filename = "../lang/" + GetSetting("LANG") + ".json";
        if (!File.Exists(_context.Server.MapPath(filename)))
            filename = "../lang/en.json";
        return filename;
    }
    protected string LangRes(string name)
    {
        isAuthenticated();
        string ret = name;
        if (_lang == null)
            _lang = ParseJSON(GetLangFile());
        if (_lang.ContainsKey(name))
            ret = _lang[name];

        return ret;
    }
    protected string GetFileType(string ext){
        isAuthenticated();
        string ret = "file";
        ext = ext.ToLower();
        if(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
            ret = "image";
          else if(ext == ".swf" || ext == ".flv")
            ret = "flash";
        return ret;
    }
    protected bool CanHandleFile(string filename)
    {
        isAuthenticated();
        bool ret = false;
        //FileInfo file = new FileInfo(filename);
        string ext = Path.GetExtension(filename).ToLower().Replace(".", "");
        //string ext = file.Extension.Replace(".", "").ToLower();
        string setting = GetSetting("FORBIDDEN_UPLOADS").Trim().ToLower();
        if (setting != "")
        {
            ArrayList tmp = new ArrayList();
            tmp.AddRange(Regex.Split(setting, "\\s+"));
            if (!tmp.Contains(ext))
                ret = true;
        }
        setting = GetSetting("ALLOWED_UPLOADS").Trim().ToLower();
        if (setting != "")
        {
            ArrayList tmp = new ArrayList();
            tmp.AddRange(Regex.Split(setting, "\\s+"));
            if (!tmp.Contains(ext))
                ret = false;
        }
        
        return ret;
    }
    protected Dictionary<string, string> ParseJSON(string file){
        isAuthenticated();
        Dictionary<string, string> ret = new Dictionary<string,string>();
        string json = "";
        try{
            json = File.ReadAllText(_context.Server.MapPath(file), System.Text.Encoding.UTF8);
        }
        catch(Exception ex){}

        json = json.Trim();
        if(json != ""){
            if (json.StartsWith("{"))
                json = json.Substring(1, json.Length - 2);
            json = json.Trim();
            json = json.Substring(1, json.Length - 2);
            string[] lines = Regex.Split(json, "\"\\s*,\\s*\"");
            foreach(string line in lines){
                string[] tmp = Regex.Split(line, "\"\\s*:\\s*\"");
                try{
                    if (tmp[0] != "" && !ret.ContainsKey(tmp[0]))
                    {
                       ret.Add(tmp[0], tmp[1]);
                    }
                }
                catch(Exception ex){}
            }
        }
        return ret;
    }
    protected string GetFilesRoot(){
        isAuthenticated();
        string ret = GetSetting("FILES_ROOT");
        if (GetSetting("SESSION_PATH_KEY") != "" && _context.Session[GetSetting("SESSION_PATH_KEY")] != null)
            ret = (string)_context.Session[GetSetting("SESSION_PATH_KEY")];
        
        if(ret == "")
            ret = _context.Server.MapPath("../Uploads");
        else
            ret = FixPath(ret);
        return ret;
    }
    protected void LoadConf(){
        isAuthenticated();
        if(_settings == null)
            _settings = ParseJSON(confFile);
    }
    protected string GetSetting(string name){
        isAuthenticated();
        string ret = "";
        LoadConf();
        if(_settings.ContainsKey(name))
            ret = _settings[name];
        
        return ret;
    }
    protected void CheckPath(string path)
    {
        isAuthenticated();
        if (FixPath(path).IndexOf(GetFilesRoot()) != 0)
        {
            throw new Exception("Access to " + path + " is denied");
        }
    }
    protected void VerifyAction(string action)
    {
        isAuthenticated();
        string setting = GetSetting(action);
        if (setting.IndexOf("?") > -1)
            setting = setting.Substring(0, setting.IndexOf("?"));
        if (!setting.StartsWith("/"))
            setting = "/" + setting;
        setting = ".." + setting;
        
        if (_context.Server.MapPath(setting) != _context.Server.MapPath(_context.Request.Url.LocalPath))
            throw new Exception(LangRes("E_ActionDisabled"));
    }
    protected string GetResultStr(string type, string msg)
    {
        isAuthenticated();
        return "{\"res\":\"" + type + "\",\"msg\":\"" + msg.Replace("\"","\\\"") + "\"}";
    }
    protected string GetSuccessRes(string msg)
    {
        isAuthenticated();
        return GetResultStr("ok", msg);
    }
    protected string GetSuccessRes()
    {
        isAuthenticated();
        return GetSuccessRes("");
    }
    protected string GetErrorRes(string msg)
    {
        isAuthenticated();
        return GetResultStr("error", msg);
    }
    private void _copyDir(string path, string dest){
        isAuthenticated();
        if(!Directory.Exists(dest))
            Directory.CreateDirectory(dest);
        foreach(string f in  Directory.GetFiles(path)){
            //FileInfo file = new FileInfo(f);
            string nomeArquivo = Path.GetFileName(f);
            if (!File.Exists(Path.Combine(dest, nomeArquivo))){
                File.Copy(f, Path.Combine(dest, nomeArquivo));
            }
        }
        foreach (string d in Directory.GetDirectories(path))
        {
            DirectoryInfo dir = new DirectoryInfo(d);
            _copyDir(d, Path.Combine(dest, dir.Name));
        }
    }
    protected void CopyDir(string path, string newPath)
    {
        isAuthenticated();
        CheckPath(path);
        CheckPath(newPath);
        DirectoryInfo dir = new  DirectoryInfo(FixPath(path));
        DirectoryInfo newDir = new DirectoryInfo(FixPath(newPath + "/" + dir.Name));
        
        if (!dir.Exists)
        {
            throw new Exception(LangRes("E_CopyDirInvalidPath"));    
        }
        else if (newDir.Exists)
        {
            throw new Exception(LangRes("E_DirAlreadyExists"));
        }
        else{
            _copyDir(dir.FullName, newDir.FullName);
        }
        _r.Write(GetSuccessRes());
    }
    protected string MakeUniqueFilename(string dir, string filename){
        isAuthenticated();
        string ret = filename;
        int i = 0;
        while (File.Exists(Path.Combine(dir, ret)))
        {
            i++;
            ret = Path.GetFileNameWithoutExtension(filename) + " - Copy " + i.ToString() + Path.GetExtension(filename);
        }
        return ret;
    }
    protected void CopyFile(string path, string newPath)
    {
        isAuthenticated();
        CheckPath(path);
        //FileInfo file = new FileInfo(FixPath(path));
        string file = FixPath(path);
        newPath = FixPath(newPath);
        if (!File.Exists(file))
            throw new Exception(LangRes("E_CopyFileInvalisPath"));
        else{
            string newName = MakeUniqueFilename(newPath, Path.GetFileName(file));
            try{
                File.Copy(file, Path.Combine(newPath, newName));
                _r.Write(GetSuccessRes());
            }
            catch(Exception ex){
                throw new Exception(LangRes("E_CopyFile"));
            }
        }
    }
    protected void CreateDir(string path, string name)
    {
        isAuthenticated();
        CheckPath(path);
        path = FixPath(path);
        if(!Directory.Exists(path))
            throw new Exception(LangRes("E_CreateDirInvalidPath"));
        else{
            try
            {
                path = Path.Combine(path, name);
                if(!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                _r.Write(GetSuccessRes());
            }
            catch (Exception ex)
            {
                throw new Exception(LangRes("E_CreateDirFailed"));
            }
        }
    }
    protected void DeleteDir(string path)
    {
        isAuthenticated();
        CheckPath(path);
        path = FixPath(path);
        if (!Directory.Exists(path))
            throw new Exception(LangRes("E_DeleteDirInvalidPath"));
        else if (path == GetFilesRoot())
            throw new Exception(LangRes("E_CannotDeleteRoot")); 
        else if(Directory.GetDirectories(path).Length > 0 || Directory.GetFiles(path).Length > 0)
            throw new Exception(LangRes("E_DeleteNonEmpty"));
        else
        {
            try
            {
                Directory.Delete(path);
                _r.Write(GetSuccessRes());
            }
            catch (Exception ex)
            {
                throw new Exception(LangRes("E_CannotDeleteDir"));
            }
        }
    }
    protected void DeleteFile(string path)
    {
        isAuthenticated();
        CheckPath(path);
        path = FixPath(path);
        if (!File.Exists(path))
            throw new Exception(LangRes("E_DeleteFileInvalidPath"));
        else
        {
            try
            {
                File.Delete(path);
                _r.Write(GetSuccessRes());
            }
            catch (Exception ex)
            {
                throw new Exception(LangRes("E_DeletеFile"));
            }
        }
    }
    private List<string> GetFiles(string path, string type){
        isAuthenticated();
        List<string> ret = new List<string>();
        if(type == "#")
            type = "";
        string[] files = Directory.GetFiles(path);
        foreach(string f in files){
            if ((GetFileType(Path.GetExtension(f)) == type) || (type == ""))
                ret.Add(f);
        }
        return ret;
    }
    private ArrayList ListDirs(string path){
        isAuthenticated();
        string[] dirs = Directory.GetDirectories(path);
        ArrayList ret = new ArrayList();
        foreach(string dir in dirs){
            ret.Add(dir);
            ret.AddRange(ListDirs(dir));
        }
        return ret;
    }
    protected void ListDirTree(string type)
    {
        isAuthenticated();
        DirectoryInfo d = new DirectoryInfo(GetFilesRoot());
        if(!d.Exists)
            throw new Exception("Invalid files root directory. Check your configuration.");
            
        ArrayList dirs = ListDirs(d.FullName);
        dirs.Insert(0, d.FullName);
        
        string localPath = _context.Server.MapPath("~/");
        _r.Write("[");
        for(int i = 0; i <dirs.Count; i++){
            string dir = (string) dirs[i];
            _r.Write("{\"p\":\"/" + dir.Replace(localPath, "").Replace("\\", "/") + "\",\"f\":\"" + GetFiles(dir, type).Count.ToString() + "\",\"d\":\"" + Directory.GetDirectories(dir).Length.ToString() + "\"}");
            if(i < dirs.Count -1)
                _r.Write(",");
        }
        _r.Write("]");
    }
    protected double LinuxTimestamp(DateTime d){
        isAuthenticated();
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
        TimeSpan timeSpan = (d.ToLocalTime() - epoch);
        
        return timeSpan.TotalSeconds;

    }
    protected void ListFiles(string path, string type)
    {
        isAuthenticated();
        CheckPath(path);
        string fullPath = FixPath(path);
        List<string> files = GetFiles(fullPath, type);
        _r.Write("[");
        for(int i = 0; i < files.Count; i++){
            string f = (files[i]);
            int w = 0, h = 0;
            if (GetFileType(Path.GetExtension(files[i])) == "image")
            {
                try{
                    FileStream fs = new FileStream(files[i], FileMode.Open);
                    Image img = Image.FromStream(fs);
                    w = img.Width;
                    h = img.Height;
                    fs.Close();
                    fs.Dispose();
                    img.Dispose();
                }
                catch(Exception ex){throw ex;}
            }
            _r.Write("{");
            _r.Write("\"p\":\"" + path + "/" + Path.GetFileName(files[i]) + "\"");
            _r.Write(",\"t\":\"" + Math.Ceiling(LinuxTimestamp(File.GetLastWriteTime(f))).ToString() + "\"");
            _r.Write(",\"s\":\""+f.Length.ToString()+"\"");
            _r.Write(",\"w\":\""+w.ToString()+"\"");
            _r.Write(",\"h\":\""+h.ToString()+"\"");
            _r.Write("}");
            if (i < files.Count - 1)
                _r.Write(",");
        }
        _r.Write("]");
    }
    //public void DownloadDir(string path)
    //{
    //    isAuthenticated();
    //    path = FixPath(path);
    //    if(!Directory.Exists(path))
    //        throw new Exception(LangRes("E_CreateArchive"));
    //    string dirName = new FileInfo(path).Name;
    //    string tmpZip = _context.Server.MapPath("../tmp/" + dirName + ".zip");
    //    if(File.Exists(tmpZip))
    //        File.Delete(tmpZip);
        
    //    System.IO.Compression.ZipFileZipFile.CreateFromDirectory(path, tmpZip,CompressionLevel.Fastest, true);
    //    _r.Clear();
    //    _r.Headers.Add("Content-Disposition", "attachment; filename=\"" + dirName + ".zip\"");
    //    _r.ContentType = "application/force-download";
    //    _r.TransmitFile(tmpZip);
    //    _r.Flush();
    //    File.Delete(tmpZip);
    //    _r.End();
    //}
    protected void DownloadFile(string path)
    {
        isAuthenticated();
        CheckPath(path);
        //FileInfo file = new FileInfo(FixPath(path));
        string file = FixPath(path);
        if(File.Exists(file)){
            _r.Clear();
            _r.Headers.Add("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(file) + "\"");
            _r.ContentType = "application/force-download";
            _r.TransmitFile(file);
            _r.Flush();
            _r.End();
        }
    }
    protected void MoveDir(string path, string newPath)
    {
        isAuthenticated();
        CheckPath(path);
        CheckPath(newPath);
        DirectoryInfo source = new DirectoryInfo(FixPath(path));
        DirectoryInfo dest = new DirectoryInfo(FixPath(Path.Combine(newPath, source.Name)));
        if(dest.FullName.IndexOf(source.FullName) == 0)
            throw new Exception(LangRes("E_CannotMoveDirToChild"));
        else if (!source.Exists)
            throw new Exception(LangRes("E_MoveDirInvalisPath"));
        else if (dest.Exists)
            throw new Exception(LangRes("E_DirAlreadyExists"));
        else{
            try{
                source.MoveTo(dest.FullName);
                _r.Write(GetSuccessRes());
            }
            catch(Exception ex){
                throw new Exception(LangRes("E_MoveDir") + " \"" + path + "\"");
            }
        }
        
    }
    protected void MoveFile(string path, string newPath)
    {
        isAuthenticated();
        CheckPath(path);
        CheckPath(newPath);
        string source = (FixPath(path));
        string dest = (FixPath(newPath));
        if (!File.Exists(source))
            throw new Exception(LangRes("E_MoveFileInvalisPath"));
        else if (File.Exists(dest))
            throw new Exception(LangRes("E_MoveFileAlreadyExists"));
        else
        {
            try
            {
                File.Move(source,dest);
                //source.MoveTo(dest.FullName);
                _r.Write(GetSuccessRes());
            }
            catch (Exception ex)
            {
                throw new Exception(LangRes("E_MoveFile") + " \"" + path + "\"");
            }
        }
    }
    protected void RenameDir(string path, string name)
    {
        isAuthenticated();
        CheckPath(path);
        DirectoryInfo source = new DirectoryInfo(FixPath(path));
        DirectoryInfo dest = new DirectoryInfo(Path.Combine(source.Parent.FullName, name));
        if(source.FullName == GetFilesRoot())
            throw new Exception(LangRes("E_CannotRenameRoot"));
        else if (!source.Exists)
            throw new Exception(LangRes("E_RenameDirInvalidPath"));
        else if (dest.Exists)
            throw new Exception(LangRes("E_DirAlreadyExists"));
        else
        {
            try
            {
                source.MoveTo(dest.FullName);
                _r.Write(GetSuccessRes());
            }
            catch (Exception ex)
            {
                throw new Exception(LangRes("E_RenameDir") + " \"" + path + "\"");
            }
        }
    }
    protected void RenameFile(string path, string name)
    {
        isAuthenticated();
        CheckPath(path);
        string source = (FixPath(path));
        string dest = (Path.Combine(Path.GetDirectoryName(source), name));
        if (!File.Exists(source))
            throw new Exception(LangRes("E_RenameFileInvalidPath"));
        else if (!CanHandleFile(name))
            throw new Exception(LangRes("E_FileExtensionForbidden"));
        else
        {
            try
            {
                File.Move(source,dest);
                _r.Write(GetSuccessRes());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "; " + LangRes("E_RenameFile") + " \"" + path + "\"");
            }
        }
    }
    public bool ThumbnailCallback()
    {
        
        return false;
    }

    protected void ShowThumbnail(string path, int width, int height)
    {
        isAuthenticated();
        CheckPath(path);
        FileStream fs = new FileStream(FixPath(path), FileMode.Open);
        Bitmap img = new Bitmap(Bitmap.FromStream(fs));
        fs.Close();
        fs.Dispose();
        int cropWidth = img.Width, cropHeight = img.Height;
        int cropX = 0, cropY = 0;

        double imgRatio = (double)img.Width / (double)img.Height;
        
        if(height == 0)
            height = Convert.ToInt32(Math.Floor((double)width / imgRatio));

        if (width > img.Width)
            width = img.Width;
        if (height > img.Height)
            height = img.Height;

        double cropRatio = (double)width / (double)height;
        cropWidth = Convert.ToInt32(Math.Floor((double)img.Height * cropRatio));
        cropHeight = Convert.ToInt32(Math.Floor((double)cropWidth / cropRatio));
        if (cropWidth > img.Width)
        {
            cropWidth = img.Width;
            cropHeight = Convert.ToInt32(Math.Floor((double)cropWidth / cropRatio));
        }
        if (cropHeight > img.Height)
        {
            cropHeight = img.Height;
            cropWidth = Convert.ToInt32(Math.Floor((double)cropHeight * cropRatio));
        }
        if(cropWidth < img.Width){
            cropX = Convert.ToInt32(Math.Floor((double)(img.Width - cropWidth) / 2));
        }
        if(cropHeight < img.Height){
            cropY = Convert.ToInt32(Math.Floor((double)(img.Height - cropHeight) / 2));
        }

        Rectangle area = new Rectangle(cropX, cropY, cropWidth, cropHeight);
        Bitmap cropImg = img.Clone(area, System.Drawing.Imaging.PixelFormat.DontCare);
        img.Dispose();
        Image.GetThumbnailImageAbort imgCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

        _r.AddHeader("Content-Type", "image/png");
        cropImg.GetThumbnailImage(width, height, imgCallback, IntPtr.Zero).Save(_r.OutputStream, ImageFormat.Png);
        _r.OutputStream.Close();
        cropImg.Dispose();
    }
    private ImageFormat GetImageFormat(string filename){
        isAuthenticated();
        ImageFormat ret = ImageFormat.Jpeg;
        switch(Path.GetExtension(filename).ToLower()){
            case ".png": ret = ImageFormat.Png; break;
            case ".gif": ret = ImageFormat.Gif; break;
        }
        return ret;
    }
    protected void ImageResize(string path, string dest, int width, int height)
    {
        isAuthenticated();
        FileStream fs = new FileStream(path, FileMode.Open);
        Image img = Image.FromStream(fs);
        fs.Close();
        fs.Dispose();
        float ratio = (float)img.Width / (float)img.Height;
        if ((img.Width <= width && img.Height <= height) || (width == 0 && height == 0))
            return;

        int newWidth = width;
        int newHeight = Convert.ToInt16(Math.Floor((float)newWidth / ratio));
        if ((height > 0 && newHeight > height) || (width == 0))
        {
            newHeight = height;
            newWidth = Convert.ToInt16(Math.Floor((float)newHeight * ratio));
        }
        Bitmap newImg = new Bitmap(newWidth, newHeight);
        Graphics g = Graphics.FromImage((Image)newImg);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.DrawImage(img, 0, 0, newWidth, newHeight);
        img.Dispose();
        g.Dispose();
        if(dest != ""){
            newImg.Save(dest, GetImageFormat(dest));
        }
        newImg.Dispose();
    }
    protected bool IsAjaxUpload()
    {
        isAuthenticated();
        return (_context.Request["method"] != null && _context.Request["method"].ToString() == "ajax");
    }
    protected void Upload(string path)
    {
        isAuthenticated();
        CheckPath(path);
        path = FixPath(path);
        string res = GetSuccessRes();
        bool hasErrors = false;
        try{
            for(int i = 0; i < HttpContext.Current.Request.Files.Count; i++){
                if (CanHandleFile(HttpContext.Current.Request.Files[i].FileName))
                {
                    string f = (HttpContext.Current.Request.Files[i].FileName);
                    string filename = MakeUniqueFilename(path, Path.GetFileName(f));
                    string dest = Path.Combine(path, filename);
                    HttpContext.Current.Request.Files[i].SaveAs(dest);
                    if (GetFileType(Path.GetExtension(filename)) == "image")
                    {
                        int w = 0;
                        int h = 0;
                        int.TryParse(GetSetting("MAX_IMAGE_WIDTH"), out w);
                        int.TryParse(GetSetting("MAX_IMAGE_HEIGHT"), out h);
                        ImageResize(dest, dest, w, h);
                    }
                }
                else
                {
                    hasErrors = true;
                    res = GetSuccessRes(LangRes("E_UploadNotAll"));
                }
            }
        }
        catch(Exception ex){
            res = GetErrorRes(ex.Message);
        }
        if (IsAjaxUpload())
        {
            if(hasErrors)
                res = GetErrorRes(LangRes("E_UploadNotAll"));
            _r.Write(res);
        }
        else
        {
            _r.Write("<script>");
            _r.Write("parent.fileUploaded(" + res + ");");
            _r.Write("</script>");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
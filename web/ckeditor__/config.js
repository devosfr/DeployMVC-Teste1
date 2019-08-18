/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    var roxyFileman = '/ckeditor/fileman/index.html';


    //$(function () {
        config.filebrowserBrowseUrl = roxyFileman;
        config.filebrowserImageBrowseUrl = roxyFileman + '?type=image';
        config.filebrowserUploadUrl = roxyFileman + '?type=file';
        config.removeDialogTabs = 'link:upload;image:upload';
    //});
};

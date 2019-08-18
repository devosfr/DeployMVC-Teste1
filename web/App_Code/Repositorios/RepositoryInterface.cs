using NHibernate;
using NHibernate.Linq;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

public class Repository<T> where T : class
{
    private readonly ISession _session = NHibernateHelper.CurrentSession;
    private ITransaction _transaction;

    public Repository(ISession session)
    {
        _session = session;

    }



    #region IRepository<T> Members

    public bool Add(T entity)
    {
        _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        _session.Save(entity);
        _transaction.Commit();
        return true;
    }

    public bool Add(System.Collections.Generic.IEnumerable<T> items)
    {
        _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        foreach (T item in items)
        {
            _session.Save(item);
        }
        _transaction.Commit();
        return true;
    }

    public bool Update(System.Collections.Generic.IEnumerable<T> items)
    {
        _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        foreach (T item in items)
        {
            _session.Update(item);
        }
        _transaction.Commit();
        return true;
    }

    public bool Update(T entity)
    {
        _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        _session.Update(entity);
        _transaction.Commit();
        return true;
    }

    public bool Delete(T entity)
    {
        _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        _session.Delete(entity);
        _transaction.Commit();
        return true;
    }

    public bool Delete(System.Collections.Generic.IEnumerable<T> entities)
    {
        _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        foreach (T entity in entities)
        {
            _session.Delete(entity);
        }
        _transaction.Commit();
        return true;
    }

    #endregion

    #region IIntKeyedRepository<T> Members

    public T FindBy(int id)
    {
        return _session.Get<T>(id);
    }

    #endregion

    #region IReadOnlyRepository<T> Members

    public IQueryable<T> All()
    {
        return _session.Query<T>();
    }

    public T FindBy(System.Linq.Expressions.Expression<System.Func<T, bool>> expression)
    {
        return FilterBy(expression).FirstOrDefault();
    }

    public IQueryable<T> FilterBy(System.Linq.Expressions.Expression<System.Func<T, bool>> expression)
    {
        return All().Where(expression).AsQueryable();
    }
    #endregion
}


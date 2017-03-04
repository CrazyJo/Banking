using System;
using System.Data.Entity;

namespace Banking.Data.Infrastructure
{
    public interface IDbFactory
    {
        DbContext GetContext();
    }
}

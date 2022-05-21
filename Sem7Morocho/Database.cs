using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Sem7Morocho
{
    public interface Database
    {
        SQLiteAsyncConnection GetConnection();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Data;
using Microsoft.EntityFrameworkCore;

namespace dotNet_wepApi_entityFrameWork
{
    public static class Utils
    {
        public static void RemoveWhere<T>(DataContext db, Expression<Func<T, bool>> filter)
            where T : class
        {
            string selectSql = db.Set<T>().Where(filter).ToQueryString();
            int fromIndex = selectSql.IndexOf("FROM");
            int whereIndex = selectSql.IndexOf("WHERE");

            string fromSql = selectSql.Substring(fromIndex, whereIndex - fromIndex);
            string whereSql = selectSql.Substring(whereIndex);
            string aliasSQl =
                fromSql.IndexOf(" AS ") > -1 ? fromSql.Substring(fromSql.IndexOf(" AS ") + 4) : "";
            string deleteSql = string.Join(
                " ",
                "DELETE ",
                aliasSQl.Trim(),
                fromSql.Trim(),
                whereSql.Trim()
            );
            db.Database.ExecuteSqlRaw(deleteSql);
        }
    }
}

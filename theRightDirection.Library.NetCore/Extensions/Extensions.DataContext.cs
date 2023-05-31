using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection
{
    public static partial class Extensions
    {
        ///// <summary>
        ///// retrieve all tables which are defined in a EntityFramework DataContext
        ///// </summary>
        ///// <param name="ctx"></param>
        ///// <returns></returns>
        //public static IQueryable<TableInfo> GetTablesInfo(this DbContext ctx)
        //{
        //    Expression query = null;
        //    IQueryProvider provider = null;

        //    var ctxConst = Expression.Constant(ctx);
        //    var groupingKey = Expression.Constant(1);

        //    // gathering information for MemberInit creation 
        //    var newExpression = Expression.New(typeof(TableInfo).GetConstructor(Type.EmptyTypes));
        //    var tableNameProperty = typeof(TableInfo).GetProperty(nameof(TableInfo.TableName));
        //    var recordCountProperty = typeof(TableInfo).GetProperty(nameof(TableInfo.RecordCount));

        //    foreach (var entityType in ctx.Model.GetEntityTypes())
        //    {
        //        var entityParam = Expression.Parameter(entityType.ClrType, "e");
        //        var tableName = entityType.GetTableName();

        //        // ctx.Set<entityType>()
        //        var setQuery = Expression.Call(ctxConst, nameof(DbContext.Set), new[] { entityType.ClrType });

        //        // here we initialize IQueryProvider, which is needed for creating final query
        //        provider ??= ((IQueryable)Expression.Lambda(setQuery).Compile().DynamicInvoke()).Provider;

        //        // grouping paraneter has generic type, we have to specify it
        //        var groupingParameter = Expression.Parameter(typeof(IGrouping<,>).MakeGenericType(typeof(int), entityParam.Type), "g");

        //        // g => new TableInfo { TableName = "tableName", RecordCount = g.Count() }
        //        var selector = Expression.MemberInit(newExpression,
        //            Expression.Bind(tableNameProperty, Expression.Constant(tableName)),
        //            Expression.Bind(recordCountProperty,
        //                Expression.Call(typeof(Enumerable), nameof(Enumerable.Count), new[] { entityParam.Type }, groupingParameter)));

        //        // ctx.Set<entityType>.GroupBy(e => 1)
        //        var groupByCall = Expression.Call(typeof(Queryable), nameof(Queryable.GroupBy), new[]
        //            {
        //            entityParam.Type,
        //            typeof(int)
        //        },
        //            setQuery,
        //            Expression.Lambda(groupingKey, entityParam)
        //        );

        //        // ctx.Set<entityType>.GroupBy(e => 1).Select(g => new TableInfo { TableName = "tableName",  RecordCount = g.Count()}))
        //        groupByCall = Expression.Call(typeof(Queryable), nameof(Queryable.Select),
        //            new[] { groupingParameter.Type, typeof(TableInfo) },
        //            groupByCall,
        //            Expression.Lambda(selector, groupingParameter));

        //        // generate Concat if needed
        //        if (query != null)
        //            query = Expression.Call(typeof(Queryable), nameof(Queryable.Concat), new[] { typeof(TableInfo) }, query,
        //                groupByCall);
        //        else
        //            query = groupByCall;
        //    }

        //    // unusual situation, but Model can have no registered entities
        //    if (query == null)
        //        return null;

        //    return provider.CreateQuery<TableInfo>(query);
        //}
    }
}

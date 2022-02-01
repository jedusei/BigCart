using System;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace BigCart.UITests.Extensions
{
    public static class AppExtensions
    {
        public static AppResult QueryOne(this IApp app, Query query)
        {
            return app.Query(query).FirstOrDefault();
        }

        public static T QueryOne<T>(this IApp app, Func<AppQuery, AppTypedSelector<T>> query)
        {
            return app.Query(query).FirstOrDefault();
        }
    }
}

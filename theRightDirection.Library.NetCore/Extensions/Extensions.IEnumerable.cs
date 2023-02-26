using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection
{
    public static partial class Extensions
    {
        /// <summary>
        /// converteer een ienumerable naar een dictionary met controle
        /// of keys al bestaan in de dictionary. Indien dat het geval is wordt
        /// er een logmelding weggeschreven
        /// </summary>
        /// <typeparam name="T">type voor de key</typeparam>
        /// <typeparam name="U">type voor de value</typeparam>
        /// <typeparam name="V">type van de lijst</typeparam>
        /// <param name="list">de lijst om te converteren naar een dictionary</param>
        /// <param name="key">de eigenschap van een object gebruikt mag worden als key</param>
        /// <param name="property">de eigenschap van een object gebruikt mag worden als value</param>
        /// <returns>dictionary van type T,U</returns>
        public static Dictionary<T, U> ToDictionaryWithSafeGuard<T, U, V>(this IEnumerable<V> list, Func<V, T> key, Func<V, U> property)
        {
            var result = new Dictionary<T, U>();
            list.ForEach(x =>
            {
                var keyValue = key(x);
                var value = property(x);
                if (!result.ContainsKey(keyValue))
                {
                    result.Add(keyValue, value);
                }
            });
            return result;
        }
    }
}
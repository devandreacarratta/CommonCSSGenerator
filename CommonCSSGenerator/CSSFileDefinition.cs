using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CommonCSSGenerator
{
    public class CSSFileDefinition
    {
        public SortedDictionary<string, CSSElements> Items = null;

        public CSSFileDefinition()
        {
            this.Items = new SortedDictionary<string, CSSElements>();
        }

        public void Add(string key, string value, string originalValue)
        {
            if (this.Items.ContainsKey(key) == false)
            {
                this.Items.Add(key, new CSSElements());
            }

            int items = this.Items[key]
                .Where(x => x.Key == value)
                .Count();

            if (items == 0)
            {
                CSSElement element = new CSSElement(string.Empty, value, originalValue);

                this.Items[key].Add(element);
            }
        }

[JsonIgnore]
        public SortedDictionary<string, int> KeysCounter
        {
            get
            {
                SortedDictionary<string, int> result = new SortedDictionary<string, int>();

                foreach (var item in Items)
                {
                    var keys = item.Value.Select(x => x.Key).Distinct().ToList();

                    foreach (var k in keys)
                    {
                        if (result.ContainsKey(k) == false)
                        {
                            result.Add(k, 0);
                        }
                        result[k] += 1;
                    }
                }

                return result;
            }
        }

    }

}
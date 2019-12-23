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

        public void Add(string key, string value, string originalValue, int line)
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
                CSSElement element = new CSSElement(string.Empty, value, originalValue, line);

                this.Items[key].Add(element);
            }
        }

        public SortedDictionary<string, int> _keysCounter = null;

        public SortedDictionary<string, int> KeysCounter(bool reset = false)
        {
            if (reset || _keysCounter == null)
            {
                _keysCounter = new SortedDictionary<string, int>();

                foreach (var item in Items)
                {
                    var keys = item.Value.Select(x => x.Key).Distinct().ToList();

                    foreach (var k in keys)
                    {
                        if (_keysCounter.ContainsKey(k) == false)
                        {
                            _keysCounter.Add(k, 0);
                        }
                        _keysCounter[k] += 1;
                    }
                }
            }
            return _keysCounter;
        }
    }
}
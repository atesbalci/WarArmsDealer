using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public enum ResourceType
    {
        Bok
    }

    /// <summary>
    /// A resource manager class to get resources from.
    /// Makes tracking which clases are loaded from code easier.
    /// </summary>
    public class WResources : Singleton<WResources>
    {
        private Dictionary<ResourceType, string> _resources;

        protected override void Init()
        {
            base.Init();
            _resources = new Dictionary<ResourceType, string>
            {
                { ResourceType.Bok, "Bok" }
            };
        }

        public T Load<T>(ResourceType resource) where T : Object
        {
            return Resources.Load<T>(_resources[resource]);
        }

        public GameObject Load(ResourceType resource)
        {
            return Load<GameObject>(resource);
        }
    }
}

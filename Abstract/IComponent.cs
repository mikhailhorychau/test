using UnityEngine;

namespace UIScripts.Abstract
{
    public interface IComponent : IGameObject
    {
        
    }

    public interface IGameObject
    {
        public GameObject GameObject { get; }
    }
}
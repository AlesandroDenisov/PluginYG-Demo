using System.Collections;
using UnityEngine;

namespace HomoLudens.Core
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
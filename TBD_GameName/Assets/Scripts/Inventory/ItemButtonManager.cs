using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Inventory
{

    public class ItemButtonManager : MonoBehaviour
    {
        [SerializeField] private UnityEvent mouseOnIt;
        [SerializeField] private UnityEvent mouseOffIt;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnMouseEnter()
        {
            mouseOnIt?.Invoke();
        }

        private void OnMouseExit()
        {
            mouseOffIt?.Invoke();
        }

        public void AddListenerToMouseOnItem(UnityAction action)
        {
            mouseOnIt.AddListener(action);
        }

        public void AddListenerToMouseOffItem(UnityAction action)
        {
            mouseOffIt.AddListener(action);
        }

        public void AddListenerToMouseOnClick(UnityAction action)
        {
            button.onClick.AddListener(action);
        }
    }
}


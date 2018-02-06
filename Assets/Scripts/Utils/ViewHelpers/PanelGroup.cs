using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Utils.ViewHelpers
{
    public class PanelGroup : MonoBehaviour
    {
        public PanelGroupElement[] Panels;

        private bool[] _panelStates;

        private void Start()
        {
            _panelStates = Panels.Select(x => x.Active).ToArray();
        }

        private void LateUpdate()
        {
            for (var i = 0; i < Panels.Length; i++)
            {
                if (Panels[i].Active != _panelStates[i])
                {
                    if (Panels[i].Active)
                    {
                        foreach (var panel in Panels)
                        {
                            panel.Active = false;
                        }
                        Panels[i].Active = true;
                    }
                    _panelStates[i] = Panels[i].Active;
                }
            }
        }
    }
}

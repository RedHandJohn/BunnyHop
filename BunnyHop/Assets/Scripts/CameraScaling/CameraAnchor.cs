/* 
 * Script source: https://gamedevelopertips.com/how-to-handle-different-aspect-ratios-in-unity-a-complete-guide/
 * Changed most of it.
 */

/***
 * This script will anchor a GameObject to a relative screen position.
 * This script is intended to be used with CameraFit.cs by Marcel Căşvan, available here: http://gamedev.stackexchange.com/a/89973/50623
 * 
 * Note: For performance reasons it's currently assumed that the game resolution will not change after the game starts.
 * You could not make this assumption by periodically calling UpdateAnchor() in the Update() function or a coroutine, but is left as an exercise to the reader.
 */
/* The MIT License (MIT)

Copyright (c) 2015, Eliot Lash

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BunnyHop.CameraScaling
{
    [ExecuteInEditMode]
    public class CameraAnchor : MonoBehaviour
    {
        public CameraAnchorType AnchorType;

        private Vector3 _anchorVector;
        [SerializeField]
        private Vector3 _offset;
        public Vector3 Offset { get { return _offset; } }


        public void SetAnchorVector(Vector3 anchorVector)
        {
            _anchorVector = anchorVector;

            UpdateAnchoredPosition();
        }

        public void SetInitialPosition()
        {
            _offset = transform.position - _anchorVector;
        }


        private void UpdateAnchoredPosition()
        {
            if (!transform.position.Equals(_offset + _anchorVector))
            {
                transform.position = _offset + _anchorVector;
            }
        }
    }
}

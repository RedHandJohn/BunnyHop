/* 
 * Original script source https://gamedevelopertips.com/how-to-handle-different-aspect-ratios-in-unity-a-complete-guide/
 * I made some modifications but I've used this script before and it's a great resource. Highly recommended.
 */

/* The MIT License (MIT)

Copyright (c) 2014, Marcel Căşvan

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BunnyHop.CameraScaling
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class CameraFit : MonoBehaviour
	{
		public float UnitsForWidth = 1; // width of your scene in unity units
		public List<CameraAnchor> AnchoredObjects;

		private Camera _camera;

		private float _width;
		private float _height;
		// bottom screen
		private Vector3 _bottomLeft;
		private Vector3 _bottomCenter;
		private Vector3 _bottomRight;
		// middle screen
		private Vector3 _middleLeft;
		private Vector3 _middleCenter;
		private Vector3 _middleRight;
		// top screen
		private Vector3 _topLeft;
		private Vector3 _topCenter;
		private Vector3 _topRight;

		// update detection
		private float _currentCameraAspect;
		private float _currrentUnitsForWidth;

		#region METHODS
		private void Awake()
		{
			Debug.Log("CameraFit.Awake");
			try
			{
				_camera = GetComponent<Camera>();
				if (_camera != null && _camera.orthographic)
				{
					ComputeResolution();
				}
			}
			catch (Exception e)
			{
				Debug.LogException(e, this);
			}
		}

		private void ComputeResolution()
		{
			Debug.Log("CameraFit.ComputeResolution");

			float leftX, rightX, topY, bottomY;

			/* Set the ortograpish size (shich is half of the vertical size) when we change the ortosize of the camera the item will be scaled 
			 * autoamtically to fit the size frame of the camera
			 */
			_camera.orthographicSize = 1f / _camera.aspect * UnitsForWidth / 2f;

			//Get the new height and Widht based on the new orthographicSize
			_height = 2f * _camera.orthographicSize;
			_width = _height * _camera.aspect;

			float cameraX, cameraY;
			cameraX = _camera.transform.position.x;
			cameraY = _camera.transform.position.y;

			leftX = cameraX - _width / 2;
			rightX = cameraX + _width / 2;
			topY = cameraY + _height / 2;
			bottomY = cameraY - _height / 2;

			//*** bottom
			_bottomLeft = new Vector3(leftX, bottomY, 0);
			_bottomCenter = new Vector3(cameraX, bottomY, 0);
			_bottomRight = new Vector3(rightX, bottomY, 0);
			//*** middle
			_middleLeft = new Vector3(leftX, cameraY, 0);
			_middleCenter = new Vector3(cameraX, cameraY, 0);
			_middleRight = new Vector3(rightX, cameraY, 0);
			//*** top
			_topLeft = new Vector3(leftX, topY, 0);
			_topCenter = new Vector3(cameraX, topY, 0);
			_topRight = new Vector3(rightX, topY, 0);

			_currentCameraAspect = _camera.aspect;
			_currrentUnitsForWidth = UnitsForWidth;

			UpdateAnchors();
		}

		private void UpdateAnchors()
        {
			Debug.Log("CameraFit.UpdateAnchoredObjects");
			foreach (var anchoredObject in AnchoredObjects)
            {
				Vector3 anchorVector = GetAnchorVector(anchoredObject.AnchorType);
				anchoredObject.SetAnchorVector(anchorVector);
			}
        }

		private Vector3 GetAnchorVector(CameraAnchorType anchorType)
        {
			Debug.Log("CameraAnchor.GetAnchorVector");
			Vector3 anchor = Vector3.zero;
			switch (anchorType)
			{
				case CameraAnchorType.BottomLeft:
					anchor = _bottomLeft;
					break;
				case CameraAnchorType.BottomCenter:
					anchor = _bottomCenter;
					break;
				case CameraAnchorType.BottomRight:
					anchor = _bottomRight;
					break;
				case CameraAnchorType.MiddleLeft:
					anchor = _middleLeft;
					break;
				case CameraAnchorType.MiddleCenter:
					anchor = _middleCenter;
					break;
				case CameraAnchorType.MiddleRight:
					anchor = _middleRight;
					break;
				case CameraAnchorType.TopLeft:
					anchor = _topLeft;
					break;
				case CameraAnchorType.TopCenter:
					anchor = _topCenter;
					break;
				case CameraAnchorType.TopRight:
					anchor = _topRight;
					break;
				case CameraAnchorType.Undefined:
					anchor = Vector3.zero;
					break;
			}
			return anchor;
		}


		private void Update()
		{
#if UNITY_EDITOR
			if(_currentCameraAspect != _camera.aspect || _currrentUnitsForWidth != UnitsForWidth)
            {
				Debug.Log("CameraFit.Update");
				ComputeResolution();
			}
#endif
		}

		private void OnDrawGizmos()
		{
			if (_camera.orthographic)
			{
				DrawGizmos();
			}
		}

		private void DrawGizmos()
		{
			//*** bottom
			Gizmos.DrawIcon(_bottomLeft, "point.png", false);
			Gizmos.DrawIcon(_bottomCenter, "point.png", false);
			Gizmos.DrawIcon(_bottomRight, "point.png", false);
			//*** middle
			Gizmos.DrawIcon(_middleLeft, "point.png", false);
			Gizmos.DrawIcon(_middleCenter, "point.png", false);
			Gizmos.DrawIcon(_middleRight, "point.png", false);
			//*** top
			Gizmos.DrawIcon(_topLeft, "point.png", false);
			Gizmos.DrawIcon(_topCenter, "point.png", false);
			Gizmos.DrawIcon(_topRight, "point.png", false);

			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(_bottomLeft, _bottomRight);
			Gizmos.DrawLine(_bottomRight, _topRight);
			Gizmos.DrawLine(_topRight, _topLeft);
			Gizmos.DrawLine(_topLeft, _bottomLeft);
		}
		#endregion
	}
}
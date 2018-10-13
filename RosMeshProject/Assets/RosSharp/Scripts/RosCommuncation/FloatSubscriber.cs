using System;
using System.Threading;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
	public class FloatSubscriber : Subscriber<Messages.Standard.Float32>
	{
		public float messageData;
		private bool isMessageReceived = false;

		protected override void Start()
		{
			base.Start();
		}

		private void Update()
		{
			if (isMessageReceived)
			Debug.Log ("Conected"+messageData);
		}


		protected override void ReceiveMessage(Messages.Standard.Float32 message)
		{
			messageData = message.data;
			isMessageReceived = true;
		}
	}
}

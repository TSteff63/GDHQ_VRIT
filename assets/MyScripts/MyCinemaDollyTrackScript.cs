using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine
{
    public class MyCinemaDollyTrackScript : CinemachineDollyCartEditedCopy
    {
        public float initialSpeed;
        public float endSpeed;
        public float maxLerpTime = 1f;
        private float timer;

        [SerializeField]
        private CinemachineSmoothPath[] paths;

        //called from timeline
        public void AdjustSpeed(float _speed)
        {
            timer = 0;
            initialSpeed = m_Speed;
            endSpeed = _speed;
        }

        public void AdjustDoubleSpeed()
        {
            timer = 0;
            initialSpeed = m_Speed;
            endSpeed = (m_Speed * 2);
        }

        public void AdjustHalfSpeed()
        {
            timer = 0;
            initialSpeed = m_Speed;
            endSpeed = (m_Speed / 2);
        }

        public void AdjustIncreaseSpeedByTwo()
        {
            timer = 0;
            initialSpeed = m_Speed;
            endSpeed = (m_Speed + 2);
        }

        public void AdjustDecreaseSpeedByTwo()
        {
            timer = 0;
            initialSpeed = m_Speed;
            endSpeed = (m_Speed + 2);
        }

        public void ChangePath(int i)
        {
            m_Path = paths[i];
        }

        public void ResetPosition(float i)
        {
            m_Position = i;
        }



        protected override void Update()
        {
            base.Update();

            //increment timer once per frame until it reaches max
            if (timer < maxLerpTime)
            {
                timer += Time.deltaTime;
                //lerp!
                float perc = timer / maxLerpTime;
                m_Speed = Mathf.Lerp(initialSpeed, endSpeed, perc);
                Debug.Log("current speed is - " + m_Speed + "initial speed is - " + initialSpeed);
            }

            if (timer > maxLerpTime)
            {
                timer = maxLerpTime;
            }
        }

    }
}
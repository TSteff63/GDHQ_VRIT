﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine
{
    public class MyCinemaDollyTrackScript : CinemachineDollyCart
    {
        public float initialSpeed;
        public float endSpeed;
        public float maxLerpTime = 1f;
        private float timer;



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
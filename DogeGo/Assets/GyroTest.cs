using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;


    /// <summary>  
    /// 职责:   
    ///     1.实现陀螺仪对相机的影响和操作；  
    ///     2.尽量重现崩坏3的主界面驾驶舱效果；  
    /// </summary>  
    class GyroTest : MonoBehaviour
    {

        #region 声明  

        /// <summary> 陀螺仪的输入类型 </summary>  
        public enum EGyroInputType
        {
            /// <summary> RotateRate </summary>  
            RotateRate,

            /// <summary> RotateRateUniased </summary>  
            RotateRateUniased,

            /// <summary> UserAcceleration </summary>  
            UserAcceleration,
        }

        #endregion



        #region 控制变量  

        public float m_gyro_max_x = 15.0f;

        public float m_gyro_max_y = 15.0f;

        public float m_gyro_max_z = 15.0f;

        #endregion



        #region 变量  

        /// <summary> editor开发环境下的模拟陀螺仪输入 </summary>  
        public Vector3 m_editor_debug_input = Vector3.zero;

        /// <summary> 陀螺仪的输入参数，用以控制相机 </summary>  
        public Vector3 m_gyro_input = Vector3.zero;

        /// <summary> 当前的摄像机角度 </summary>  
        public Vector3 m_cur_euler = Vector3.zero;

        /// <summary> 陀螺仪数据的更新频率 </summary>  
        public int m_upate_rate = 30;

        /// <summary> 当前陀螺仪的输入输入类型 </summary>  
        public EGyroInputType m_gyro_input_type = EGyroInputType.RotateRate;

        /// <summary> 陀螺仪的系数 </summary>  
        public float m_gyro_factor = 1.0f;

        private Vector3 m_camera_init_euler = Vector3.zero;
        private Transform mTransform;
        #endregion



        #region 访问接口  

        /// <summary> 陀螺仪的输入参数，用以控制相机 </summary>  
        protected Vector3 GyroInput
        {
            get
            {
                return m_gyro_input;
            }
            set
            {
                m_gyro_input = value;
            }
        }

        /// <summary> 陀螺仪输入数据的类型 </summary>  
        protected EGyroInputType GyroInputType
        {
            get
            {
                return m_gyro_input_type;
            }
            set
            {
                m_gyro_input_type = value;
            }
        }

        /// <summary> 陀螺仪的系数 </summary>  
        protected float GyroFactor
        {
            get
            {
                return m_gyro_factor;
            }
            set
            {
                m_gyro_factor = value;
            }
        }

        /// <summary> 当前的旋转角 </summary>  
        protected Vector3 CurEuler
        {
            get
            {
                return m_cur_euler;
            }
            set
            {
                m_cur_euler = value;
            }
        }

        #endregion



        #region Unity  

        // Use this for initialization  
        void Start()
        {
            Input.gyro.enabled = true;

            mTransform = gameObject.transform;
            CurEuler = mTransform.localEulerAngles;
            m_camera_init_euler = CurEuler;
        }

        /// <summary> 绘制UI，方便调试 </summary>  
        void OnGUI()
        {
            GUI.Label(GetRect(0.1f, 0.05f), "Attitude: " + Input.gyro.attitude);  

            GUI.Label(GetRect(0.1f, 0.15f), "Rotation: " + Input.gyro.rotationRate);  

            GUI.Label(GetRect(0.1f, 0.25f), "RotationUnbiased: " + Input.gyro.rotationRateUnbiased);  

            GUI.Label(GetRect(0.1f, 0.35f), "UserAcceleration: " + Input.gyro.userAcceleration);  

            //// 陀螺仪的系数  
            //{  
            //    string t_factor_str = GUI.TextField(GetRect(0.7f, 0.05f), "" + GyroFactor);  

            //    GyroFactor = float.Parse(t_factor_str);  
            //}  

            //// 陀螺仪输入参数  
            //{  
            //    if (GUI.Button(GetRect(0.8f, 0.8f, 0.2f), "" + GyroInputType))  
            //    {  
            //        switch (GyroInputType)  
            //        {  
            //            case EGyroInputType.RotateRate:  
            //                GyroInputType = EGyroInputType.RotateRateUniased;  
            //                break;  

            //            case EGyroInputType.RotateRateUniased:  
            //                GyroInputType = EGyroInputType.UserAcceleration;  
            //                break;  

            //            case EGyroInputType.UserAcceleration:  
            //                GyroInputType = EGyroInputType.RotateRate;  
            //                break;  
            //        }  
            //    }  
            //}  
        }

        // Update is called once per frame  
        void Update()
        {
            // 设置陀螺仪更新频率  
            Input.gyro.updateInterval = 1.0f / m_upate_rate;

            // 根据陀螺仪计算相机的控制数据  
            UpdateGyro();

            // Editor下的调试  
#if UNITY_EDITOR
            // 开发环境下不能用陀螺仪，模拟数据  
            GyroInput = m_editor_debug_input;
#endif

            // 因值不确定范围，需增加系数控制  
            GyroInput = GyroInput * GyroFactor;

            // 根据控制数据，对相机进行操作和变化  
            UpdateCamera();
        }

        #endregion



        #region 控制逻辑  

        /// <summary> 更新陀螺仪数据，并计算出相应的控制数据 </summary>  
        protected void UpdateGyro()
        {
            // 更新陀螺仪数据，并计算出控制变量  
            switch (GyroInputType)
            {   //手机上左倾斜x是负值，又倾斜x是正值。上倾斜y是负值，下倾斜y是正值  
                case EGyroInputType.RotateRate:
                    GyroInput = Input.gyro.rotationRate;
                    break;

                case EGyroInputType.RotateRateUniased:
                    GyroInput = Input.gyro.rotationRateUnbiased;
                    break;

                case EGyroInputType.UserAcceleration:
                    GyroInput = Input.gyro.userAcceleration;
                    break;

                default:
                    Debug.LogError("GyroInputTypeNot defined: " + GyroInputType);
                    break;
            }
        }

        /// <summary> 更新相机的行为 </summary>  
        protected void UpdateCamera()
        {
            // 不需要gyro的z参数  
#if UNITY_EDITOR
            Vector3 t_gyro_input = new Vector3(GyroInput.x, GyroInput.y, GyroInput.z);
#else
            Vector3 t_gyro_input = new Vector3(0.0f, GyroInput.y, GyroInput.x);  
#endif

            CurEuler += t_gyro_input;

            // 范围控制  
            {
                float t_x = ClampFloat(CurEuler.x, m_camera_init_euler.x, m_gyro_max_x);

                float t_y = ClampFloat(CurEuler.y, m_camera_init_euler.y, m_gyro_max_y);

                float t_z = ClampFloat(CurEuler.z, m_camera_init_euler.z, m_gyro_max_z);

                CurEuler = new Vector3(t_x, t_y, t_z);
            }

            mTransform.localEulerAngles = CurEuler;
        }


        #endregion



        #region 支持函数  

        protected float ClampFloat(float p_float, float p_init, float p_offset)
        {
            p_offset = Mathf.Abs(p_offset);

            if (p_float > p_init + p_offset)
            {
                p_float = p_init + p_offset;
            }

            if (p_float < p_init - p_offset)
            {
                p_float = p_init - p_offset;
            }

            return p_float;
        }

        /// <summary> 根据百分比获取gui的大概坐标 </summary>  
        protected Rect GetRect(float p_x_percent, float p_y_percent, float p_w = 0.5f, float p_h = 0.1f)
        {
            return new Rect(
                Screen.width * p_x_percent, Screen.height * p_y_percent,
                Screen.width * p_w, Screen.height * p_h);
        }

        #endregion

    }


using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ZhengHua
{ 
    public class LoadManager : SingtonMono<LoadManager>
    {
        private Image loadingImage; // 載入圖片
        private TMP_Text _loadText;
        private TMP_Text _readyText;
        private TMP_Text _tipText;
        private Canvas _canvas;
        private bool _needDestory = false;
        private AsyncOperation _operation;
        private LoadState _state = LoadState.Idle;

        public override void Awake()
        {
            base.Awake();
            if (_canvas == null)
            {
                _canvas = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/LoadCanvas")).GetComponent<Canvas>();
            }
            DontDestroyOnLoad(_canvas);
            DontDestroyOnLoad(this); // 保證載入畫面在場景切換時不被銷毀

            foreach (TMP_Text text in _canvas.GetComponentsInChildren<TMP_Text>())
            {
                if (text.name == "LoadText")
                    _loadText = text;
                else if (text.name == "ReadyText")
                    _readyText = text;
                else if (text.name == "TipText")
                    _tipText = text;
            }

            _canvas.enabled = false;
        }

        public void LoadScene(string sceneName, bool needDestory = false)
        {
            LoadSceneAsync(sceneName);
            _needDestory = needDestory;
        }

        private void LoadSceneAsync(string sceneName)
        {
            _state = LoadState.Load;
            _canvas.enabled = true;
            _loadText.enabled = true;
            _readyText.enabled = false;
            // 載入資源
            _operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            _operation.allowSceneActivation = false;
        }

        private void Update()
        {
            if (_state == LoadState.Load)
            {
                if (_operation.progress >= 0.9f)
                {
                    _loadText.enabled = false;
                    _readyText.enabled = true;

                    if (Input.anyKeyDown)
                    {
                        if (_needDestory)
                            _instance = null;
                        _operation.allowSceneActivation = true;
                        _state = LoadState.Loaded;
                    }
                }
                else
                {
                    //Debug.Log(_operation.progress / 0.9f);
                }
            }

            if (_state == LoadState.Loaded)
            {
                if (_operation.isDone)
                {
                    _canvas.enabled = false;
                    _state = LoadState.Idle;
                    // 銷毀載入畫面
                    if (_needDestory)
                    {
                        Destroy(_canvas.gameObject);
                        Destroy(gameObject);
                    }
                }
            }
        }

        private enum LoadState
        {
            Idle,
            Load,
            Loaded,
        }
    }
}
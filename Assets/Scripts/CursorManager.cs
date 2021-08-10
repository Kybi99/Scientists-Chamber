using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance { get; private set; }
        [SerializeField] private List<CursorAnimation> cursorAnimationList;
        private CursorAnimation cursorAnimation;
        private int frameCount;
        private int currentFrame;
        private float frameTimer;

        public enum CursorType
        {
            Arrow,
            Door,
            Portal,
            DoorFixed,
            HandFixed
        }
        private void Start()
        {
            SetActiveCursorType(CursorType.Arrow);
        }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        private void Update()
        {
            frameTimer -= Time.deltaTime;
            if (frameTimer <= 0f)
            {
                frameTimer += cursorAnimation.frameRate;
                currentFrame = (currentFrame + 1) % frameCount;
                Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.hotspot, CursorMode.ForceSoftware);
            }
        }

        public void SetActiveCursorType(CursorType cursorType)
        {
            SetActiveCursorAnimation(GetCursorAnimation(cursorType));
        }

        private CursorAnimation GetCursorAnimation(CursorType cursorType)
        {
            foreach (CursorAnimation cursorAnimation in cursorAnimationList)
            {
                if (cursorAnimation.cursorType == cursorType)
                {
                    return cursorAnimation;
                }
            }
            return null;
        }
        private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
        {
            this.cursorAnimation = cursorAnimation;
            currentFrame = 0;
            frameTimer = cursorAnimation.frameRate;
            frameCount = cursorAnimation.textureArray.Length;
        }

        [System.Serializable]
        public class CursorAnimation
        {
            public CursorType cursorType;
            public Texture2D[] textureArray;
            public float frameRate;
            public Vector2 hotspot;

        }
    }
}

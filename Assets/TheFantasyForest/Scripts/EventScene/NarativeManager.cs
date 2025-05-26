using UnityEngine;

public class NarativeManager : MonoBehaviour
{
    [SerializeField] private AnimationManagerSceneEvent _animationManager;
    [SerializeField] private TextManagerEventScene _textManager;
    [SerializeField] private ScenarioEventScene _scenario;

    private int _indexScene = 0;
    private int _indexSpriteFrame = 0;
    private int _indexTextFrame = 0;

    private void Start()
    {
        _scenario = EventSceneManager.Instance.ScenarioEventScene;
        NextSpriteFrame();
        NextTextFrame();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {          
            if (_indexScene < _scenario.Scenes.Length)
            {               
                if (_indexTextFrame < _scenario.Scenes[_indexScene].Text.Length || _indexSpriteFrame < _scenario.Scenes[_indexScene].Sprites.Length)
                {
                    NextSpriteFrame();
                    NextTextFrame();
                }else
                {
                    _indexScene++;
                    _indexSpriteFrame = 0;
                    _indexTextFrame = 0;
                    if (_indexScene < _scenario.Scenes.Length)
                    {
                        NextSpriteFrame();
                        NextTextFrame();
                    }
                }
            }
            else
            {
                for(int i = 0; i < _scenario.GameEvent.Length;i++)
                {
                    _scenario.GameEvent[i].StartEvent();
                }
            }
        }
    }

    private void NextTextFrame()
    {     
        if (_indexTextFrame < _scenario.Scenes[_indexScene].Text.Length)
        {
            SetText(_scenario.Scenes[_indexScene].Text[_indexTextFrame]);
        
            if (_indexTextFrame < _scenario.Scenes[_indexScene].Text.Length)
            {
                _indexTextFrame++;
            }
        }
    }

    private void NextSpriteFrame()
    {       
        if (_indexSpriteFrame < _scenario.Scenes[_indexScene].Sprites.Length)
        {
            SetSprite(_scenario.Scenes[_indexScene].Sprites[_indexSpriteFrame]);
        
            if (_indexSpriteFrame < _scenario.Scenes[_indexScene].Sprites.Length)
            {
                _indexSpriteFrame++;
            }
        }
    }

    private void SetSprite(Sprite sprite)
    {
        _animationManager.SpriteRenderer.sprite = sprite;
    }
    private void SetText(string text)
    {
        _textManager.Text.text = text;
    }
}

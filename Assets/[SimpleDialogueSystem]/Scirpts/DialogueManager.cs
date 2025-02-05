using FSF.Collection;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace FSF.DialogueSystem
{
    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        [SerializeField] private GameObject _imageSwitcherPrefab;
        [SerializeField] private DialogueProfile _profile;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private KeyCode[] _activationKeys = 
        { KeyCode.Space, KeyCode.Return, KeyCode.F };
        [Space(14f)]
        [SerializeField] private RectTransform _charactersHolder;
        private List<Character> _characterDisplays =new();
        [SerializeField] private Character _background;


        private int _currentIndex;
        [HideInInspector] public bool AllowInput = true;

        // 输入检测属性
        private bool InputReceived => AllowInput && (
            Input.GetMouseButtonDown(0) || 
            _activationKeys.Any(Input.GetKeyDown)
        );

        private void Start() => ShowNextDialogue();

        private void Update()
        {
            if (InputReceived)
            {
                ShowNextDialogue();
            }
        }

        public void ShowNextDialogue()
        {
            if (_profile == null || _currentIndex >= _profile.actions.Length)
            {
                ResetDialogue();
                return;
            }

            var currentAction = _profile.actions[_currentIndex];
            
            if (TypeWriter.Instance.OutputText(currentAction.name, currentAction.dialogue))
            {
                UpdateCharacterDisplays(currentAction);
                UpdateBackground(currentAction);
                PlayAudio(currentAction);
            }
            else
            {
                InterruptCharacterActions();
            }

            _currentIndex++;
        }

        private void UpdateCharacterDisplays(SingleAction action)
        {
            foreach (var option in action.imageOptions)
            {
                
                var character = _characterDisplays.FirstOrDefault(
                    _display => _display.characterDefindID == option.characterDefindID
                );
                if(character == default)
                {
                    character = InstantiateCharacter(option);
                }

                character.OutputImage(option.characterImage);
                character.Animate(option);
            }
        }

        private Character InstantiateCharacter(CharacterOption option)
        {
            var temp = Instantiate(_imageSwitcherPrefab, _charactersHolder);
            var rt = temp.transform as RectTransform;
            rt.anchoredPosition = new(-2000, -1500);
            rt.sizeDelta = new(0, 1100);

            var component = temp.GetComponent<Character>();
            component.characterDefindID = option.characterDefindID;
            _characterDisplays.Add(component);
            return component;
        }

        private void UpdateBackground(SingleAction action)
        {
            if (action.backGround != null)
            {
                _background.OutputImage(action.backGround);
            }
        }

        private void PlayAudio(SingleAction action)
        {
            if (action.audio == null) return;

            _audioSource.Stop();
            _audioSource.clip = action.audio;
            _audioSource.Play();
        }

        private void InterruptCharacterActions()
        {
            foreach (var display in _characterDisplays)
            {
                display.Interrupt();
            }
            _background.Interrupt();
        }

        public void ResetDialogue()
        {
            _currentIndex = 0;
        }
    }
}
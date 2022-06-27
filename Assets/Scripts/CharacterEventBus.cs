using UnityEngine.Events;
using System.Collections.Generic;

namespace WedoStudios.Reegan.UnityTest
{
    public enum CharacterEventType
    {
        ENEMY_DAMAGED,
        ENEMY_DEAD,

        PLAYER_DAMAGED,
        PLAYER_DEAD,
    }

    public class CharacterEventBus
    {
        private static readonly IDictionary<CharacterEventType, UnityEvent>
        Events = new Dictionary<CharacterEventType, UnityEvent>();

        public static void Subscribe(CharacterEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(eventType, thisEvent);
            }
        }
        public static void Unsubscribe(CharacterEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }
        public static void Publish(CharacterEventType eventType)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}
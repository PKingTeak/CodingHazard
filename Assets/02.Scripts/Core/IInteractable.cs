using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public bool IsLockInteract { get; set; }
    public void Interact();
}

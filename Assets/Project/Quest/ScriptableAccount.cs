using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Account", menuName = "MyAccount")]
public class ScriptableAccount : ScriptableObject
{
    [SerializeField]
    internal Account account;
    public void SetScriptable(Account account)
    {
        this.account = account;
    }
    public Account GetScriptable() { return account;}
}

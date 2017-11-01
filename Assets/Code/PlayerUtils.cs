using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public partial class PlayerUtils : NetworkBehaviour {

    // Use this for initialization
    void Start()
    {
        PlayerCommands.playerUtils = this;
    }
}

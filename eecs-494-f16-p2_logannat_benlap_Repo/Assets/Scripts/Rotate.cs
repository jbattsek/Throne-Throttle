// -----------------------------------------------------------------------------
//  Copyright © 2016 Schell Games, LLC. All Rights Reserved.
//
//  Contact: Ben Lapid
//
//  Created: 1/27/2016 1:30:00 PM
// -----------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float speed;

	void Update () {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}

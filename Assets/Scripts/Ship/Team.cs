using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class Team
    {
        public TeamTag TeamTag => this.teamTag;

        [SerializeField] private TeamTag teamTag;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Displays the current node that is running
/// </summary>
public class ShowNodeTreeStatus : MonoBehaviour
{
    private Selector tree;
    private Transform origin;

    public void AddConstructor(Transform origin, Selector tree)
    {
        this.origin = origin;
        this.tree = tree;
    }

    private void OnDrawGizmos()
    {
        string info = "";
        List<BTBaseNode> nodes = new List<BTBaseNode>(tree.Children);
        for(int i = 0; i < nodes.Count; i++)
        {
            if(nodes[i].GetType().IsEquivalentTo(typeof(Sequence)))
            {
                Sequence s = (Sequence)nodes[i];
                nodes.AddRange(s.Children);
            }
            else if(nodes[i].GetType().IsEquivalentTo(typeof(Selector)))
            {
                Selector s = (Selector)nodes[i];
                nodes.AddRange(s.Children);
            }

            info += "\n" + nodes[i].GetType().Name + ": " + nodes[i].status;
        }
        GUI.color = Color.black;
        Handles.Label(origin.position + Vector3.up * 4, info);
    }
}
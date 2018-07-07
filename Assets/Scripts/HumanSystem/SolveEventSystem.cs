using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveEventSystem{

    private HumanSystem _person = null;
    private float rayline = 50;
    public SolveEventSystem(HumanSystem person) {
        _person = person;
    }

    public void getEventCollision() {
        Vector2 targetPos = getTargetPosition();
        RaycastHit2D hit = Physics2D.Linecast(_person.transform.position, targetPos, 1 << LayerMask.NameToLayer("Furniture"));
        if (hit) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Event solve
                //var hit.collider.transform
            }
        }
    }

    private Vector2 getTargetPosition() {
        if (_person == null) {
            Debug.LogError("EventListener Lost the person target!");
            return Vector2.zero;
        }

        Vector2 pos = _person.transform.position;
        Vector2 tarPos = Vector2.zero;
        Direction dic = _person.direction;
        switch (dic) { 
            case Direction.Left:
                tarPos = new Vector2(pos.x - rayline, pos.y);
                break;
            case Direction.Up:
                tarPos = new Vector2(pos.x, pos.y + rayline);
                break;
            case Direction.Right:
                tarPos = new Vector2(pos.x - rayline, pos.y);
                break;
            case Direction.Down:
                tarPos = new Vector2(pos.x, pos.y - rayline);
                break;
        }
        return tarPos;
    }


    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_person.transform.position, getTargetPosition());
    }
}

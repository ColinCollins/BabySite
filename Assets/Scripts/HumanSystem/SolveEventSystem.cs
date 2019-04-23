using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveEventSystem {

    private HumanSystem _player = null;
	private HumanStuff _playerStuff = null;
	private TipsManager _tips = null;
    private float rayline = 3;

    public SolveEventSystem(HumanSystem person) {
		_player = person;
		_playerStuff = person.getStuffComp();
		_tips = TipsManager.getInstance();
	}

    public void getEventCollision() {
        Vector2 targetPos = getTargetPosition();
        RaycastHit2D hit = Physics2D.Linecast(_player.transform.position, targetPos, 1 << LayerMask.NameToLayer("EventStuff"));
        if (hit) {
            if (Input.GetKeyDown(KeyCode.Space)) {
				GameObject target = hit.collider.gameObject;
				//Debug.Log(target.name);
				BasicStuff stuff = target.GetComponent<BasicStuff>();
				if (stuff == null) {
					Debug.LogWarning("This stuff lost its component: " + target.name);
					return;
				}
				if (stuff.GetEventState() == EventState.Stop && (stuff.ID < 100 || stuff.isTool))
					stuff.dealWithEvent(_playerStuff);
				else if (stuff.GetEventState() == EventState.Waiting)
					stuff.waitBroken(_playerStuff);
				else
					_tips.setTips("什么事也没有发生");
			}
		}
    }

    private Vector2 getTargetPosition() {
        if (_player == null) {
            Debug.LogError("EventListener Lost the person target!");
            return Vector2.zero;
        }

        Vector2 pos = _player.transform.position;
        Vector2 tarPos = Vector2.zero;
        Direction dic = _player.direction;
        switch (dic) { 
            case Direction.Left:
                tarPos = new Vector2(pos.x - rayline, pos.y);
                break;
            case Direction.Up:
                tarPos = new Vector2(pos.x, pos.y + rayline);
                break;
            case Direction.Right:
                tarPos = new Vector2(pos.x + rayline, pos.y);
                break;
            case Direction.Down:
                tarPos = new Vector2(pos.x, pos.y - rayline);
                break;
        }
        return tarPos;
    }
}

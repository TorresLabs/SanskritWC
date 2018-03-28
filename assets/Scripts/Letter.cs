using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour {

	private Vector3 startingPosition;
	private Transform myParent;

	private List<Transform> touchingTiles = new List<Transform>();
    private AudioSource audSource;

	private SpriteRenderer render;

    private void Awake()
    {
        Debug.Log("LetterScriptAwake");
        startingPosition = transform.position;
		myParent = transform.parent;

		render = gameObject.GetComponent<SpriteRenderer> ();
        audSource = gameObject.GetComponent<AudioSource>();
    }

    public void PickUp()
    {
        Debug.Log("LetterPickup");
		transform.localScale = 11 * Vector3.one;
       	//gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void Drop()
    {
        Debug.Log("LetterDrop");
		transform.localScale = 10 * Vector3.one;
		//gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

		if (touchingTiles.Count == 0) {
			transform.position = startingPosition;
			transform.parent = myParent;
		} else {
			Transform currentCell = touchingTiles [0];

			if (touchingTiles.Count > 1) {
				var distance = Vector2.Distance (transform.position, touchingTiles [0].position);

				foreach (Transform cell in touchingTiles) {
					if (Vector2.Distance (transform.position, cell.position) < distance) {
						currentCell = cell;
						distance = Vector2.Distance (transform.position, cell.position);
					}
				}
			}

			if (currentCell.childCount == 0) {
				StartCoroutine (SlotIntoPlace (currentCell));
			}

			transform.position = startingPosition;
			transform.parent = myParent;
		}

    }

	void OnMouseDown() {
		Debug.Log ("OnMouseDown");
		PickUp ();
	}

	void OnMouseDrag() {
		Debug.Log ("OnMouseDrag ="+touchingTiles.Count);
		//Vector2 touchPoint = (Input.touchCount > 0) ? Input.GetTouch(0).position : new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector3 screenPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance (Camera.main.transform.position, transform.position));
		float zpos = transform.position.z;
		Vector3 newPosition = Camera.main.ScreenToWorldPoint(screenPos);
		newPosition.z = zpos;
		transform.position = newPosition;
	}

	void OnMouseUp() {
		Debug.Log ("OnMouseUp");
		Drop ();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log("OnTriggerExit2D");
		if (other.GetComponent<Letter>() != null) return;
        if (!touchingTiles.Contains(other.transform))
        {
            touchingTiles.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
		Debug.Log("OnTriggerExit2D");
		if (other.GetComponent<Letter>() != null) return;
        if (touchingTiles.Contains(other.transform))
        {
            touchingTiles.Remove(other.transform);
        }
    }

	IEnumerator SlotIntoPlace(Transform currentCell)
    {
		GameObject piece = GameObject.Instantiate (gameObject, transform.position, Quaternion.identity, currentCell);
		SpriteRenderer pieceRender = piece.GetComponent<SpriteRenderer> ();
		pieceRender.sortingOrder = 1;
		pieceRender.sprite = render.sprite;
		piece.transform.localScale = 0.6f * Vector3.one;

		Destroy (piece.GetComponent<BoxCollider2D> ());
		Destroy (piece.GetComponent<Rigidbody2D> ());
		Destroy (piece.GetComponent<Letter> ());

		Vector3 startingPos = transform.position;
		render.enabled = false;

		Debug.Log("LetterSlotIntoPlace");
        float duration = 0.1f;
        float elapsedTime = 0;
		if (audSource != null) {
			audSource.Play ();
		}
        while (elapsedTime < duration)
        {
			piece.transform.position = Vector3.Lerp(startingPos, currentCell.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

		piece.transform.position = currentCell.position;

		Score.instance.AddToScore (currentCell.GetComponent<SpriteRenderer>().sprite.name);

		render.sprite = LetterHolder.instance.GetRandom ();
		render.enabled = true;

    }
}

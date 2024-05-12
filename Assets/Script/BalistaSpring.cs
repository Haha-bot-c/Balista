using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
public class BalistaSpring : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _minAnchorY = -1.5f;
    [SerializeField] private float _maxAnchorY = 7f;
    [SerializeField] private float _delay = 0.5f;

    private Coroutine _kinematicCoroutine;
    private Rigidbody _balistaRigidbody;
    private SpringJoint _springJoint;
    private Rigidbody _projectile;

    private void Start()
    {
        _springJoint = GetComponent<SpringJoint>();
        _balistaRigidbody = _springJoint.connectedBody;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeAnchor();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }

    private void ChangeAnchor()
    {
        if (_balistaRigidbody != null)
        {
            _balistaRigidbody.isKinematic = false;
        }

        ChangeSpringAnchor();
        ReleaseProjectile();
        StartKinematicCoroutine();
    }

    private void ChangeSpringAnchor()
    {
        float currentY = _springJoint.anchor.y;
        float targetY = (currentY == _minAnchorY) ? _maxAnchorY : _minAnchorY;
        _springJoint.anchor = new Vector3(_springJoint.anchor.x, targetY, _springJoint.anchor.z);
    }

    private void ReleaseProjectile()
    {
        if (_projectile != null)
        {
            _projectile.isKinematic = false;
            _projectile = null;
        }
    }

    private void StartKinematicCoroutine()
    {
        if (_kinematicCoroutine != null)
        {
            StopCoroutine(_kinematicCoroutine);
        }
        _kinematicCoroutine = StartCoroutine(MakeKinematicAfterDelay());
    }

    private void SpawnProjectile()
    {
        if (_projectile == null)
        {
            Instantiate(_projectilePrefab, _spawnPoint.position, _spawnPoint.rotation).
                TryGetComponent(out Rigidbody rigidbody);
            _projectile = rigidbody;
        }
    }

    private IEnumerator MakeKinematicAfterDelay()
    {
        yield return new WaitForSeconds(_delay);

        if (_balistaRigidbody != null)
        {
            _balistaRigidbody.isKinematic = true; 
        }
        _kinematicCoroutine = null;
    }
}

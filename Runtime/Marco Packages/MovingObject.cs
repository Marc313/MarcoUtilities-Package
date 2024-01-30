using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace MarcoUtilities
{
    /// <summary>
    /// Base class for objects that should have easy access to coroutines or async functions
    /// that move, rotate or scale the object in a smooth way.
    /// For each method, this class contains a couroutine and async variant.
    /// Note: This class may contain untested methods!
    /// </summary>
    #region Coroutines
    // Coroutines for moving and rotating
    public partial class MovingObject : MonoBehaviour
    {
        /// <summary>
        /// Moves the current object from oldPos to a new position within a given period of time.
        /// </summary>
        /// <param name="oldPos"> Old Position</param>
        /// <param name="targetPos"> Target Position </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="onDone"> Callback to invoke after the async method is done. </param>
        public IEnumerator MoveToInSecondsCoroutine(Vector3 oldPos, Vector3 targetPos, float timeInSeconds, Action onDone = null)
        {
            transform.position = oldPos;
            float distance = Vector3.Distance(oldPos, targetPos);
            float moveSpeed = distance / timeInSeconds;
            float t = 0;

            while (t < timeInSeconds)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                t += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPos;

            onDone?.Invoke();
        }

        /// <summary>
        /// Note: Untested!
        /// Moves the current object from oldPos to a new position within a given period of time.
        /// </summary>
        /// <param name="oldPos"> Old Position</param>
        /// <param name="targetPos"> Target Position </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="onDone"> Callback to invoke after the async method is done. </param>
        public IEnumerator MoveToInSecondsFromCurveCoroutine(Vector3 oldPos, Vector3 targetPos, float timeInSeconds, AnimationCurve rotationCurve, Action onDone = null)
        {
            transform.position = oldPos;
            float distance = Vector3.Distance(oldPos, targetPos);
            float moveSpeed = distance / timeInSeconds;
            float t = 0;

            while (t < timeInSeconds)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, rotationCurve.Evaluate(timeInSeconds / t));
                t += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPos;

            onDone?.Invoke();
        }

        /// <summary>
        /// Note: Untested!
        /// Rotates the current object from oldRotation to a new rotation within a given period of time in a linear way.
        /// </summary>
        /// <param name="oldRotation"> Old Rotation </param>
        /// <param name="targetRotation"> Target Rotation </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="onDone"> Callback to invoke after the async method is done. </param>
        public IEnumerator RotateTowardsInSecondsCoroutine(Quaternion oldRotation, Quaternion targetRotation, float timeInSeconds, Action onDone = null)
        {
            float angle = Quaternion.Angle(oldRotation, targetRotation);
            float rotationSpeed = angle / timeInSeconds;
            float t = 0;

            transform.rotation = oldRotation;

            while (t < timeInSeconds)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                t += Time.deltaTime;
                yield return null;
            }
            transform.rotation = targetRotation;

            onDone?.Invoke();
        }

        /// <summary>
        /// Note: Untested!
        /// Smoothly rotates the current object from oldRotation to a new rotation within a given period of time.
        /// Uses Slerp insread of RotateTowards to make the rotation look more smooth.
        /// </summary>
        /// <param name="oldRotation"> Old Rotation </param>
        /// <param name="targetRotation"> Target Rotation </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="rotationCurve"> Curve that controls the rotation animation </param>
        /// <param name="onDone"> (Optional) Callback to invoke after the async method is done. </param>
        public IEnumerator RotateInSecondsFromCurveCoroutine(Quaternion oldRotation, Quaternion targetRotation, float timeInSeconds, AnimationCurve rotationCurve, Action onDone = null)
        {
            //float angle = Quaternion.Angle(closedRotation, targetRotation);
            //float rotationSpeed = angle / timeInSeconds;
            float t = 0;

            transform.rotation = oldRotation;

            while (t < timeInSeconds)
            {
                float timeRatio = timeInSeconds / t;
                transform.rotation = Quaternion.Slerp(oldRotation, targetRotation, rotationCurve.Evaluate(t));
                t += Time.deltaTime;
                yield return null;
            }
            transform.rotation = targetRotation;

            onDone?.Invoke();
        }

        /// <summary>
        /// Note: Untested!
        /// Smoothly rotates the current object from oldRotation to a new rotation within a given period of time.
        /// Uses Slerp insread of RotateTowards to make the rotation look more smooth.
        /// </summary>
        /// <param name="oldRotation"> Old Rotation </param>
        /// <param name="targetRotation"> Target Rotation </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="onDone"> (Optional) Callback to invoke after the async method is done. </param>
        public IEnumerator RotateWithSlerpInSecondsCoroutine(Quaternion oldRotation, Quaternion targetRotation, float timeInSeconds, Action onDone = null)
        {
            //float angle = Quaternion.Angle(closedRotation, targetRotation);
            //float rotationSpeed = angle / timeInSeconds;
            float t = 0;

            transform.rotation = oldRotation;

            while (t < timeInSeconds)
            {
                float timeRatio = timeInSeconds / t;
                transform.rotation = Quaternion.Slerp(oldRotation, targetRotation, t);
                t += Time.deltaTime;
                yield return null;
            }
            transform.rotation = targetRotation;

            onDone?.Invoke();
        }

        // TODO: Implement scaling
    }
    #endregion


    #region Async
    // Async functions for moving and rotating
    public partial class MovingObject : MonoBehaviour
    {
        /// <summary>
        /// Moves the current object from oldPos to a new position within a given period of time.
        /// </summary>
        /// <param name="oldPos"> Old Position</param>
        /// <param name="targetPos"> Target Position </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="onDone"> Callback to invoke after the async method is done. </param>
        public async Task MoveToInSecondsAsync(Vector3 oldPos, Vector3 targetPos, float timeInSeconds, Action onDone = null)
        {
            transform.position = oldPos;
            float distance = Vector3.Distance(oldPos, targetPos);
            float moveSpeed = distance / timeInSeconds;
            float t = 0;

            while (t < timeInSeconds)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                t += Time.deltaTime;
                await Task.Yield();
            }
            transform.position = targetPos;

            onDone?.Invoke();
        }

        /// <summary>
        /// Note: Untested!
        /// Moves the current object from oldPos to a new position within a given period of time.
        /// </summary>
        /// <param name="oldPos"> Old Position</param>
        /// <param name="targetPos"> Target Position </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="onDone"> Callback to invoke after the async method is done. </param>
        public async Task MoveToInSecondsFromCurveAsync(Vector3 oldPos, Vector3 targetPos, float timeInSeconds, AnimationCurve rotationCurve, Action onDone = null)
        {
            transform.position = oldPos;
            float distance = Vector3.Distance(oldPos, targetPos);
            float moveSpeed = distance / timeInSeconds;
            float t = 0;

            while (t < timeInSeconds)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, rotationCurve.Evaluate(timeInSeconds / t));
                t += Time.deltaTime;
                await Task.Yield();
            }
            transform.position = targetPos;

            onDone?.Invoke();
        }

        /// <summary>
        /// Note: Untested!
        /// Rotates the current object from oldRotation to a new rotation within a given period of time in a linear way.
        /// </summary>
        /// <param name="oldRotation"> Old Rotation </param>
        /// <param name="targetRotation"> Target Rotation </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="onDone"> Callback to invoke after the async method is done. </param>
        public async Task RotateTowardsInSecondsAsync(Quaternion oldRotation, Quaternion targetRotation, float timeInSeconds, Action onDone = null)
        {
            float angle = Quaternion.Angle(oldRotation, targetRotation);
            float rotationSpeed = angle / timeInSeconds;
            float t = 0;

            transform.rotation = oldRotation;

            while (t < timeInSeconds)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                t += Time.deltaTime;
                await Task.Yield();
            }
            transform.rotation = targetRotation;

            onDone?.Invoke();
        }

        /// <summary>
        /// Note: Untested!
        /// Smoothly rotates the current object from oldRotation to a new rotation within a given period of time.
        /// Uses Slerp insread of RotateTowards to make the rotation look more smooth.
        /// </summary>
        /// <param name="oldRotation"> Old Rotation </param>
        /// <param name="targetRotation"> Target Rotation </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="rotationCurve"> Curve that controls the rotation animation </param>
        /// <param name="onDone"> (Optional) Callback to invoke after the async method is done. </param>
        public async Task RotateInSecondsFromCurveAsync(Quaternion oldRotation, Quaternion targetRotation, float timeInSeconds, AnimationCurve rotationCurve, Action onDone = null)
        {
            //float angle = Quaternion.Angle(closedRotation, targetRotation);
            //float rotationSpeed = angle / timeInSeconds;
            float t = 0;

            transform.rotation = oldRotation;

            while (t < timeInSeconds)
            {
                float timeRatio = timeInSeconds / t;
                transform.rotation = Quaternion.Slerp(oldRotation, targetRotation, rotationCurve.Evaluate(t));
                t += Time.deltaTime;
                await Task.Yield();
            }
            transform.rotation = targetRotation;

            onDone?.Invoke();
        }

        /// <summary>
        /// Note: Untested!
        /// Smoothly rotates the current object from oldRotation to a new rotation within a given period of time.
        /// Uses Slerp insread of RotateTowards to make the rotation look more smooth.
        /// </summary>
        /// <param name="oldRotation"> Old Rotation </param>
        /// <param name="targetRotation"> Target Rotation </param>
        /// <param name="timeInSeconds"> Time it takes from A to B in seconds </param>
        /// <param name="onDone"> (Optional) Callback to invoke after the async method is done. </param>
        public async Task RotateWithSlerpInSecondsAsync(Quaternion oldRotation, Quaternion targetRotation, float timeInSeconds, Action onDone = null)
        {
            //float angle = Quaternion.Angle(closedRotation, targetRotation);
            //float rotationSpeed = angle / timeInSeconds;
            float t = 0;

            transform.rotation = oldRotation;

            while (t < timeInSeconds)
            {
                float timeRatio = timeInSeconds / t;
                transform.rotation = Quaternion.Slerp(oldRotation, targetRotation, t);
                t += Time.deltaTime;
                await Task.Yield();
            }
            transform.rotation = targetRotation;

            onDone?.Invoke();
        }

        // TODO: Implement scaling
    }
    #endregion
}

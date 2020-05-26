using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



namespace Tests
{
    public class TestSuit
    {

        private cannon cn;
        private AnimationRecorder rc;


        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("KineticsConnon"));
            cn = gameGameObject.GetComponent<cannon>();
            rc = gameGameObject.GetComponentInChildren<AnimationRecorder>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(cn.gameObject);
        }


        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CannonBallMovesTest()
        {
            Vector3 initialPosition = cn.rb.transform.position;

            yield return new WaitForSeconds(0.1f);

            Assert.AreNotEqual(initialPosition, cn.rb.transform.position);
        }

        [UnityTest]
        public IEnumerator CannonBallsVelocityAndPossitionCorrelateX()
        {
            Vector3 initialPosition ;

            yield return new WaitForSeconds(1f);

            Vector3 vel = cn.rb.velocity;
            initialPosition = cn.rb.transform.position;

            yield return null;

            // patikrina ta pati tik po didesnio laiko tarpo ir su velocity pries ta frame
            Assert.GreaterOrEqual(vel.x >= 0 ? cn.rb.transform.position.x : initialPosition.x, vel.x >= 0 ? initialPosition.x : cn.rb.transform.position.x);
           
        }

        [UnityTest]
        public IEnumerator CannonBallsVelocityAndPossitionCorrelateY()
        {
            Vector3 initialPosition ;

            yield return new WaitForSeconds(1f);

            Vector3 vel = cn.rb.velocity;
            initialPosition = cn.rb.transform.position;

            yield return null;

            // patikrina ta pati tik po didesnio laiko tarpo ir su velocity pries ta frame
          
            Assert.GreaterOrEqual(vel.y >= 0 ? cn.rb.transform.position.y : initialPosition.y, vel.y >= 0 ? initialPosition.y : cn.rb.transform.position.y);
        
        }

        [UnityTest]
        public IEnumerator CannonBallsVelocityAndPossitionCorrelateZ()
        {
            Vector3 initialPosition ;

            yield return new WaitForSeconds(1f);

            Vector3 vel = cn.rb.velocity;
            initialPosition = cn.rb.transform.position;

            yield return null;

            // patikrina ta pati tik po didesnio laiko tarpo ir su velocity pries ta frame
           
            Assert.GreaterOrEqual(vel.z >= 0 ? cn.rb.transform.position.z : initialPosition.z, vel.z >= 0 ? initialPosition.z : cn.rb.transform.position.z);
        }



        [UnityTest]
        public IEnumerator CannonBallLosesAllVelocityOnHittingTheGround()
        {
            while (cn.rb.gameObject.transform.localPosition.y > 0)
                yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(0.1f);
            Assert.AreNotEqual(cn.rb.velocity, Vector3.zero);
        }

        [UnityTest]
        public IEnumerator OnHittingTheGroundStopsUpdatingLineRenderer()
        {
            while (cn.rb.gameObject.transform.localPosition.y > 0)
                yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(1f);
            Assert.IsFalse(cn.animating);
        }



        [UnityTest]
        public IEnumerator ForceArrowDirectionTestX()
        {

            yield return new WaitForSeconds(1f);


            Vector3 vel = cn.rb.velocity;

            Vector3 initialPosition = cn.rb.transform.position;


            yield return new WaitForSeconds(1f);

            vel = cn.rb.velocity;

            yield return null;

            initialPosition = cn.rb.transform.position;
            Assert.GreaterOrEqual(vel.x >= 0 ? cn.lr.GetPosition(2).x : initialPosition.x, vel.x >= 0 ? initialPosition.x : cn.lr.GetPosition(2).x);
        }

        [UnityTest]
        public IEnumerator ForceArrowDirectionTestY()
        {

            yield return new WaitForSeconds(1f);


            Vector3 vel = cn.rb.velocity;

            Vector3 initialPosition = cn.rb.transform.position;


            yield return new WaitForSeconds(1f);

            vel = cn.rb.velocity;

            yield return null;

            initialPosition = cn.rb.transform.position;
           
            Assert.GreaterOrEqual(vel.y >= 0 ? cn.lr.GetPosition(2).y : initialPosition.y, vel.y >= 0 ? initialPosition.y : cn.lr.GetPosition(2).y);
      
        }

        [UnityTest]
        public IEnumerator ForceArrowDirectionTestZ()
        {

            yield return new WaitForSeconds(1f);


            Vector3 vel = cn.rb.velocity;

            Vector3 initialPosition = cn.rb.transform.position;


            yield return new WaitForSeconds(1f);

            vel = cn.rb.velocity;

            yield return null;

            initialPosition = cn.rb.transform.position;

            Assert.GreaterOrEqual(vel.z >= 0 ? cn.lr.GetPosition(2).z : initialPosition.z, vel.z >= 0 ? initialPosition.z : cn.lr.GetPosition(2).z);
        }

        [UnityTest]
        public IEnumerator ObjectIsRecorded()
        {
            yield return null;
            Assert.True(rc.recording);
             
        }
    }
}

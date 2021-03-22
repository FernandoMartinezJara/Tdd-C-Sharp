using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Curso_TDD
{
    [TestFixture]
    public class UpdateableSpinTests
    {
        [Test]
        public void Wait_NoPulse_Return_False()
        {
            UpdateableSpin spin = new UpdateableSpin();
            bool wasPulsed = spin.Wait(TimeSpan.FromMilliseconds(10));
            Assert.IsFalse(wasPulsed);
        }

        [Test]
        public void Wait_NoPulse_Return_True()
        {
            UpdateableSpin spin = new UpdateableSpin();

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                spin.Set();
            });

            bool wasPulsed = spin.Wait(TimeSpan.FromSeconds(10));
            Assert.IsTrue(wasPulsed);
        }


        [Test]
        public void Wai50Millisec_CallIsActuallyWaitFor50Millisec()
        {
            UpdateableSpin spin = new UpdateableSpin();

            Stopwatch watcher = new Stopwatch();

            watcher.Start();

            spin.Wait(TimeSpan.FromMilliseconds(50));

            watcher.Stop();

            TimeSpan actual = TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds);
            TimeSpan leftEpsilon = TimeSpan.FromMilliseconds(50 - (50 * 0.1));
            TimeSpan rightEpsilon = TimeSpan.FromMilliseconds(50 + (50 * 0.1));

            Assert.IsTrue(actual > leftEpsilon && actual < rightEpsilon);

        }

        [Test]
        public void Wai50Millisec_UpdateAfter300Millisec_TotalWaitAprox800Millisec()
        {
            UpdateableSpin spin = new UpdateableSpin();

            Stopwatch watcher = new Stopwatch();

            watcher.Start();

            const int timeout = 500;
            const int spanBeforeUpdate = 300;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(spanBeforeUpdate);
                spin.updateTimeout();
            });


            spin.Wait(TimeSpan.FromMilliseconds(timeout));

            watcher.Stop();

            TimeSpan actual = TimeSpan.FromMilliseconds(watcher.ElapsedMilliseconds);

            const int expected = timeout + spanBeforeUpdate;

            TimeSpan left = TimeSpan.FromMilliseconds(expected - (expected * 0.1));
            TimeSpan right = TimeSpan.FromMilliseconds(expected + (expected * 0.1));

            Assert.IsTrue(actual > left && actual < right);

        }
    }

    public class UpdateableSpin
    {
        private readonly object lockObj = new object();
        private bool shouldWait = true;
        private long executionStartingTime;

        public bool Wait(TimeSpan timeout, int spinDuration=0)
        {
            updateTimeout();

            while (true)
            {
                lock (lockObj)
                {
                    if (!shouldWait)
                        return true;
                    if (DateTime.UtcNow.Ticks - executionStartingTime > timeout.Ticks)
                        return false;
                }

                Thread.Sleep(spinDuration);
            }
          
        }

        public void Set()
        {
            lock (lockObj)
            {
                shouldWait = false;
            }
        }

        public void updateTimeout()
        {
            lock (lockObj)
            {
                executionStartingTime = DateTime.UtcNow.Ticks;
            }
        }
    }

}

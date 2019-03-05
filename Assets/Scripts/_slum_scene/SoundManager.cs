using UnityEngine;

public class SoundManager : MonoBehaviour {
	
	public AudioSource dayChannel;
	public AudioSource nightChannel;
	public AudioSource morningChannel;
	

	public void UpdateSound(int minutesGone) {
		// night-morning transition 4.30 - 5.30
		// morning-day transition 7.00 - 8.00
		// day sound - 0% at 7.00, 100% at 11.00 - 15.00, 0% at 20.00 
		// night sound - 0% at 18.00, 100% at 20.00, 100% at 4.30, 0% at 5.30
		// morning sound - 0% at 4.30, 100% at 5.30 - 6.30, 0% at 8.00,

		if (minutesGone > 1200 || minutesGone < 420) { // from 20:00 to 07:00
			dayChannel.volume = 0;
		}else if (minutesGone > 660 && minutesGone < 900) { // from 11:00 to 15:00
			dayChannel.volume = 1;
		}
		else {
			if(minutesGone >= 900) // 15:00 to 20:00
				dayChannel.volume = (1200 - minutesGone) / 300f;
			else { // 7:00 to 11:00
				dayChannel.volume = (minutesGone - 420) / 240f;
			}
		}
		
		if (minutesGone > 1200 || minutesGone < 270) { // from 20:00 to 04:30
			nightChannel.volume = 1;
		}else if (minutesGone > 330 && minutesGone < 960) { // from 05:30 to 16:00
			nightChannel.volume = 0;
		}
		else {
			if(minutesGone >= 960) // 16:00 to 20:00
				nightChannel.volume = (minutesGone - 960) / 240f;
			else { // 4:30 to 5.30
				nightChannel.volume = (330 - minutesGone) / 60f;
			}
		}
		
		if (minutesGone > 480 || minutesGone < 270) { // from 8:00 to 04:30
			morningChannel.volume = 0;
		}else if (minutesGone > 330 && minutesGone < 390) { // from 05:30 to 6:30
			morningChannel.volume = 1;
		}
		else {
			if(minutesGone >= 390) // 6:30 to 8:00
				morningChannel.volume = (480 - minutesGone) / 90;
			else { // 4:30 to 5.30
				morningChannel.volume = (minutesGone - 270) / 60;
			}
		}
	}
}

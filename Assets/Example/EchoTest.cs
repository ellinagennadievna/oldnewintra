using UnityEngine;
using System.Collections;
using System;


public class EchoTest : MonoBehaviour {

  public GameObject aObj;
	public GameObject bObj;
  //public GameObject cObj;
  public GameObject dObj;
  public GameObject eObj;
  public GameObject lObj;

  int j=0;
  int t=0;
  int tt=0;

  // Use this for initialization
	IEnumerator Start () {
		WebSocket w = new WebSocket(new Uri("ws://85.119.144.254:8080"));
		yield return StartCoroutine(w.Connect());
		w.SendString("Hi there");
	//	int i=0;
		while (true)
		{
			string reply = w.RecvString();
			if (reply != null)
			{


      string[] arr = reply.Split('\t');
      if ("/rodchenko/humidity"==arr[0]) {
        var fv = Convert.ToSingle(arr[1]);
//        Debug.Log(arr[1]+' '+arr[0]);
         aObj.gameObject.transform.position= new Vector3(dObj.gameObject.transform.position.x, dObj.gameObject.transform.position.y +40f, dObj.gameObject.transform.position.z);

        if (fv>60){
          //new Vector3(131f-((fv-50)*160/50),aObj.gameObject.transform.position.y,aObj.gameObject.transform.position.z);
          //aObj.gameObject.transform.localScale = new Vector3(fv*2+50f,fv*2+50f,fv*2+50f);
          var ps = aObj.gameObject.GetComponent<ParticleSystem>();
          var main = ps.main;
          main.startSize=(fv-50)*2;
        }
        else{//aObj.gameObject.transform.localScale = new Vector3(100f,100f,100f);
             var ps = aObj.gameObject.GetComponent<ParticleSystem>();
             var main = ps.main;
             main.startSize=1f;
             aObj.gameObject.transform.position = new Vector3(131f,aObj.gameObject.transform.position.y,aObj.gameObject.transform.position.z);
           }
        }

        if ("/rodchenko/temperature"==arr[0]) {
          var fv = Convert.ToSingle(arr[1]);
          //Debug.Log(arr[1]+' '+arr[0]);
          if (fv>28){
                          var ps = aObj.gameObject.GetComponent<ParticleSystem>();
                          var main = ps.main;
                          main.startColor= new Color( 0xFF, 0x00, 0x00, 0xFF );
            }
            else{
              if (fv>23) {
                          var ps = aObj.gameObject.GetComponent<ParticleSystem>();
                          var main = ps.main;
                          main.startColor=new Color(0x00, 0x10, 0x030, 0xFF);
                       }else{
                         var ps = aObj.gameObject.GetComponent<ParticleSystem>();
                         var main = ps.main;
                         main.startColor=new Color(0x00, 0x00, 0xFF, 0xFF);
                       }

             }
          }

	  if ("/rodchenko/sound"==arr[0]) {
			 var fv = Convert.ToSingle(arr[1]);
			Debug.Log(arr[1]+' '+arr[0]);
                if (t>0){
                 tt=tt+1;
                 }
                  else{
                 tt=0;
                 }
      //  if(j==3){
      //    j=0;
        //  bObj.gameObject.transform.localScale = new Vector3(fv/100,fv/100,fv/100);
      //  } else{
       if (fv>500){
           j=0;
           if ((t==1)&(tt<5)) {
               t=0;
               tt=0;
				bObj.gameObject.transform.localScale = new Vector3(100f, 10f, 100F);//(fv/34,fv/34,fv/34);
               bObj.gameObject.GetComponent<AudioSource>().volume=1;
               }//0.3f+bObj.gameObject.transform.localScale.x*8/200;

           t=t+1;
				}else{
              if (tt>=5) {t=0;}
              j=j+1;
			     	  if (j>4) {
                   bObj.gameObject.transform.localScale = new Vector3(0F,0F,0F);
                   bObj.gameObject.GetComponent<AudioSource>().volume=0.3f;
                   j=0;
							Debug.Log ("обнуление звука");
                   }
              else   {
                   if ((j>2)){//&(bObj.gameObject.transform.localScale.x>10F)){
                   bObj.gameObject.transform.localScale = new Vector3(bObj.gameObject.transform.localScale.x*0.8f,bObj.gameObject.transform.localScale.y*0.8f,bObj.gameObject.transform.localScale.z*0.8f);
                   bObj.gameObject.GetComponent<AudioSource>().volume=bObj.gameObject.GetComponent<AudioSource>().volume*0.8f;
                   }
                     }
				     }
			  }

        if ("/rodchenko/distance"==arr[0]) {
  				var fv = Convert.ToSingle(arr[1]);
  				Debug.Log(arr[1]+' '+arr[0]);
          if ((fv<33)&(fv>3)){
            eObj.gameObject.transform.localScale = new Vector3(fv*10f,fv*10F,fv*10F);
            eObj.gameObject.transform.position= new Vector3(dObj.gameObject.transform.position.x, dObj.gameObject.transform.position.y +40f, dObj.gameObject.transform.position.z);
            //var ps = eObj.gameObject.GetComponent<ParticleSystem>();
            //var main = ps.main;
            //var sh = main.scalingMode;
          //  if (fv>10)  {sh = new GUIContent("shape");} else{
          //     sh = new GUIContent("Hierarchy");

          }
          else {eObj.gameObject.transform.localScale = new Vector3(0F,0F,0F);}
       }

       if ("/rodchenko/led"==arr[0]) {
         var fv = Convert.ToSingle(arr[1]);
         if ((fv==1)&(bObj.gameObject.transform.localScale== new Vector3(0f,0f,0f))) {lObj.gameObject.GetComponent<Light>().intensity=0;}
         else {lObj.gameObject.GetComponent<Light>().intensity=1;}
       }
//new Quaternion(
//         string [] arr = reply.Split(' ');

  //       Debug.Log("{0} words in text:", arr.Length);
//        if ("/rodchenko/sound"==arr[1]) {
//          var floatValue = Convert.ToSingle(arr[1]);
//          floatValue = floatValue;
//        Debug.Log(  arr[0]);
            //sphere.gameObject.transform.localScale = new Vector3(30F,30F,30F);
  //        }
	//			Debug.Log ("Received:" + reply);
  //      sphere.gameObject.transform.localScale = new Vector3(30F,30F,30F);
				// w.SendString("Hi there"+i++);
			}
			if (w.error != null)
			{
				Debug.LogError ("Error: "+w.error);
				break;
			}
			yield return 0;
		}
		w.Close();
	}
}

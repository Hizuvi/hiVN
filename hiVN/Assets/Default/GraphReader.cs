using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace hiVN
{

    public class GraphReader : MonoBehaviour
    {
        public MasterGraph graph;

        public TextMeshProUGUI text;
        public TextMeshProUGUI characterName;
        public Image background;
        [HideInInspector]
        public List<Image> images; //images for characters
        public Vector2 imageOffset; //the images offset from the center
        public float imageScale = 1; //rescaling the image
        [HideInInspector]
        public List<Button> buttons; //buttons for choices
        public Image choiceBackground;

        public UnityEventList[] eventList;

        [Space(10)]
        public GameObject buttonPrefab;


        //start the chain
        public void StartGraph()
        {
            graph.StartGraph();
            Next();
        }

        [HideInInspector]
        public int nextCount; //used to avoid infinite loop

        //proceeding through the dialogue
        public void Next()
        {
            //go to the next node
            graph.Continue();

            //read what it says and display it
            object read = graph.Read();
            if (read is string)
            {
                nextCount = 0;

                if ((string)read == "__EndNode")
                {
                    return;
                } //used when the endNode has been reached
            }
            else if (read is TextReturn)
            {
                nextCount = 0;

                TextReturn textReturn = (TextReturn)read; //Cast read as TextReturn

                //Change textVariables to text
                if(graph.GetMaster().textVariables.Count != 0)
                {
                    foreach (TextVariable variable in graph.GetMaster().textVariables)
                    {
                        textReturn.text = textReturn.text.Replace("<" + variable.key + ">", variable.value);
                    }
                }

                //Display text
                text.text = textReturn.text;
                text.enabled = false; // to update the mesh
                text.enabled = true; // to update the mesh

                //Display name
                if (textReturn.character != null)
                {
                    characterName.text = textReturn.character.characterName + ":";
                    characterName.color = textReturn.character.color;
                }
                else
                {
                    characterName.text = "";
                }

            }//for text
            else if (read is ImageReturn)
            {
                ImageReturn imageReturn = (ImageReturn)read;

                if (imageReturn.removeAll)
                {
                    for (int x = images.Count - 1; x > -1; x--)
                    {
                        DestroyImmediate(images[x].gameObject);
                        images.RemoveAt(x);
                    }
                    Next();
                    return;
                }//removes all of the images

                if (imageReturn.character == null)
                {
                    Debug.LogError("You cannot have an image without a character");
                    return;
                }//if there is no character throw an error

                if (imageReturn.remove)
                {

                    if (GetImageIndex(imageReturn.character.characterName) == -1)
                    {

                    }
                    else
                    {
                        DestroyImmediate(images[GetImageIndex(imageReturn.character.characterName)].gameObject);

                        for (int x = images.Count - 1; x > -1; x--)
                        {
                            if (images[x] == null)
                            {
                                images.RemoveAt(x);
                            }
                        }
                    }
                    Next();
                    return;
                }//if the image should be removed, DESTROY IT

                int dialogBoxSize = 100;

                if (GetImageIndex(imageReturn.character.characterName) == -1)
                {
                    GameObject newGameObj = new GameObject();
                    newGameObj.transform.SetParent(transform.GetChild(0).transform);
                    Image image = newGameObj.AddComponent<Image>();
                    newGameObj.name = imageReturn.character.name;
                    image.rectTransform.localScale = Vector2.one;

                    if (imageReturn.pos == ImageReturn.ImagePositions.prev)
                    {
                        RectTransform canvTransformA = image.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
                        image.rectTransform.position = new Vector2(canvTransformA.rect.width * canvTransformA.localScale.x / 2, (canvTransformA.rect.height * canvTransformA.localScale.y / 2 + dialogBoxSize * canvTransformA.localScale.y) - 40) + imageOffset;
                    }

                    newGameObj.transform.SetSiblingIndex(1);

                    images.Add(newGameObj.GetComponent<Image>());
                } //If the image does not exist, create one

                int imageIndex = GetImageIndex(imageReturn.character.characterName);
                RectTransform canvTransform = images[imageIndex].GetComponentInParent<Canvas>().GetComponent<RectTransform>();

                images[imageIndex].sprite = imageReturn.sprite;

                int pixelRes = 250;
                //edit boxsize above
                int yPos = Mathf.RoundToInt(canvTransform.rect.height * canvTransform.localScale.y / 2 + dialogBoxSize * canvTransform.localScale.y) - 40;
                int indent = 100; //How far into the screen the images are from the side

                //change the size depending on the sprite
                Vector2 scaler = new Vector2(800 / images[imageIndex].sprite.rect.width, 800 / images[imageIndex].sprite.rect.height);
                images[imageIndex].rectTransform.localScale = new Vector2(images[imageIndex].sprite.rect.width * scaler.x / pixelRes * imageScale, images[imageIndex].sprite.rect.height * scaler.x / pixelRes * imageScale);

                //set the position of the sprite
                if (imageReturn.pos == ImageReturn.ImagePositions.left)
                {
                    images[imageIndex].rectTransform.position = new Vector2(images[imageIndex].rectTransform.rect.width * canvTransform.localScale.x, yPos) + imageOffset;
                }
                else if (imageReturn.pos == ImageReturn.ImagePositions.right)
                {
                    images[imageIndex].rectTransform.position = new Vector2(canvTransform.rect.width * canvTransform.localScale.x - images[imageIndex].rectTransform.rect.width * canvTransform.localScale.x, yPos) + imageOffset;
                }
                else if (imageReturn.pos == ImageReturn.ImagePositions.center)
                {
                    images[imageIndex].rectTransform.position = new Vector2(canvTransform.rect.width * canvTransform.localScale.x / 2, yPos) + imageOffset;
                }

                Next(); //Make shure a new text displays
            }//for images
            else if (read is EventReturn)
            {
                EventReturn eventReturn = (EventReturn)read;
                bool keyFound = false;

                foreach (UnityEventList unityEvent in eventList)
                {
                    if (unityEvent.key == eventReturn.eventName)
                    {
                        keyFound = true;
                        unityEvent.unityEvent.Invoke();
                        Debug.Log("Invoke");
                    }
                }
                if (!keyFound)
                {
                    Debug.LogError("Did not find event: " + eventReturn.eventName);
                }
            }//for events
            else if (read is ChoiceReturn) //for choices
            {
                buttons.Clear();

                //cast and read the data
                ChoiceReturn choiceReturn = (ChoiceReturn)read;
                int choiceAmount = choiceReturn.strings.Length;

                if(choiceAmount > 10)
                {
                    Debug.LogError("More than ten choices");
                    return;
                }

                //create object
                for (int i = 0; i < choiceAmount; i++)
                {
                    GameObject newGameObj = Instantiate(buttonPrefab);
                    newGameObj.transform.SetParent(transform.GetChild(0).transform);
                    Button button = newGameObj.GetComponent<Button>();
                    button.GetComponentInChildren<TextMeshProUGUI>().text = choiceReturn.strings[i];
                    button.name = i.ToString();

                    buttons.Add(button);

                    button.onClick.AddListener(() =>
                    {
                        choiceReturn.node.answer = int.Parse(button.name);
                        Next();
                        foreach (Button button1 in buttons)
                        {
                            DestroyImmediate(button1.gameObject);
                        }
                        buttons.Clear();
                        choiceBackground.gameObject.SetActive(false);
                    }
                    );

                    RectTransform canvTransform = button.GetComponentInParent<Canvas>().GetComponent<RectTransform>(); //get the canvas parented to the button

                    //set position depending on amount of buttons
                    if (choiceAmount < 6)
                    {
                        newGameObj.transform.localScale = Vector2.one / 1.5f;

                        newGameObj.transform.position = new Vector2(canvTransform.rect.width * canvTransform.localScale.x / 2, -i * 70 * canvTransform.localScale.y + 370 * canvTransform.localScale.y);
                    }
                    else
                    {
                        newGameObj.transform.localScale = Vector2.one / 1.5f;

                        if (i < 5)
                        {
                            newGameObj.transform.position = new Vector2(canvTransform.rect.width * canvTransform.localScale.x / 4 + 50 * canvTransform.localScale.x, -i * 70 * canvTransform.localScale.y + 370 * canvTransform.localScale.y);
                        }
                        else
                        {

                            newGameObj.transform.position = new Vector2(canvTransform.rect.width * canvTransform.localScale.x / 4 * 3 - 50 * canvTransform.localScale.x, (-i + 5) * 70 * canvTransform.localScale.y + 370 * canvTransform.localScale.y);
                        }
                    }
                }

                //show background
                choiceBackground.gameObject.SetActive(true);
            }//for choices
            else if (read is BackgroundReturn)//for backgrounds
            {
                BackgroundReturn backgroundReturn = (BackgroundReturn)read;

                background.sprite = backgroundReturn.sprite;

                if(background.sprite != null)
                {
                    background.color = Color.white;
                }
                else
                {
                    background.color = Color.clear;
                }

                Next();
            }//for backgrounds
            else
            {
                nextCount++;
                if (nextCount > 100)
                {
                    Debug.LogError("Infinite loop detected");
                    return;
                }//to stop infinite loops

                Next();
            }//if the returned value was null then repeat next
        }

        public int GetImageIndex(string imgName)
        {
            int i = 0;

            foreach (Image image in images)
            {
                if (image.name == imgName)
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

        private void Awake()
        {
            images.Clear();
            StartGraph(); //makes shure the graph starts where is should
        }
    }


    [Serializable]
    public class UnityEventList
    {
        public string key;
        public UnityEvent unityEvent;
    }
}
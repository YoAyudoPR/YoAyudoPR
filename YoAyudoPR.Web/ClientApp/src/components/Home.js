import React, { Component, useState } from 'react';
import { Accordion } from 'react-bootstrap';
import './Home.css';

// export const App= () => {
//     const {embededURL} = useState('https://drive.google.com/file/d/1gx7KD6KqISXeojBkDzIMOjiAMysTSv7a/preview');
    
//         return(
//             <iframe src={embededURL} width="640" height="480"></iframe>
    
//         );

// }
// export default App

export class Home extends Component {
  static displayName = Home.name;

  render() {
    
      return (
          <><div class="bg-pic">
          <h1 className = "title" >Help Puerto Rico Community</h1>
          <div class="containerAccordion">
              <Accordion defaultActiveKey="0">
              <Accordion.Item eventKey="0">
                  <Accordion.Header>What is YoAyudoPR?</Accordion.Header>
                  <Accordion.Body>
                   The purpose of this project is to provide a website where high school students and college students may locate volunteering opportunities to broaden their community service experience. Allow local organizations to list their events and volunteer recruitment efforts as well.
                  </Accordion.Body>
              </Accordion.Item>
              <Accordion.Item eventKey="1">
                  <Accordion.Header>Project Synopsis</Accordion.Header>
                  <Accordion.Body>
                  <p>The project will consist of an environment where students can easily find volunteer work in a quick manner while also taking part in something they really feel compelled to do. All of this in Puerto Rico. At the same time, companies or organizations looking for volunteers can find students more effectively and record their completed work, all in the same place. </p>
                  </Accordion.Body>
                  </Accordion.Item>
                <Accordion.Item eventKey="2">
                    <Accordion.Header>Domain Model</Accordion.Header>
                    <Accordion.Body>
                       <p>Currently, all high schools in Puerto Rico require students to complete community service hours as part of their credit hours. The current process to complete these involves a lot of hand to hand, swivel-chairing work that can result in human errors, service time not being counted and service time falsely imputed. To prevent such events, the process of applying to be a volunteer, verifying the hours with an organizer or a non-profit organization, validation of hours worked and report can be all left for an application to handle. Such a system must handle all these tasks above and provide ease of use to any type of person, not regarding any technological knowledge. By achieving this vision, any individual with the desired interest to do volunteer work will now save a great amount of time from searching and validating the working hours.  A useful group to utilize the system are high school students that are required in Puerto Rico to complete a minimum of 40 hours in order to graduate. This is regarding the complications most students suffer and the time of finding a good place to complete their volunteering hours in order to graduate. Many students struggle to find a line of work that makes them motivated to really make the impact they are supposed to do when it comes to volunteering. Many schools impose the required forty hours of volunteer work but do not provide the options for students to have an accessible list of impactful volunteer work. With a simple platform to find various options and track their work, having to settle for what is the most common in the school and avoiding tedious organization of working hours can be a problem of the past.</p>
                    </Accordion.Body>
                  </Accordion.Item>
                  <Accordion.Item eventKey="3">
                      <Accordion.Header>Technical Process</Accordion.Header>
                      <Accordion.Body>
                          <p>To make the application function efficiently, students and companies alike must create an account a set an easy criteria of what they are looking for in the application and their work. After this, the matching algorithm kicks in. The matching system is applied whenever the students interest are “Match” with the company interest tags after it’s added to the interest list. An example of this system is when a company has created an event or activity with the “clean beach” (tag) every Sunday and a student has the interest “clean beach” (tag) it will recommend the event to students that have the same tag.</p>
                      </Accordion.Body>
                  </Accordion.Item>
                  <Accordion.Item eventKey="4">
                      <Accordion.Header>Project Plan</Accordion.Header>
                      <Accordion.Body>
                        <p>By mid-December, the team plans on having a functional application with most features implemented. The first step will be completing the back-end portion. Starting from the end of October to mid-November. After this, the focus will shift to testing the back-end and completing the front-end portion. This will make every feature accessible from a simple standpoint while also providing a fine user experience. This will complete the targeted development schedule by early December. From this point on, the team can focus on testing and providing a video demonstration of all the completed work. </p>
                      </Accordion.Body>
                  </Accordion.Item>
                  <Accordion.Item eventKey="5">
                      <Accordion.Header>Github Repository</Accordion.Header>
                      <Accordion.Body>
                          <p>Link to <a href='https://github.com/YoAyudoPR/YoAyudoPR'>Github Repository</a></p>
                      </Accordion.Body>
                  </Accordion.Item>
                  <Accordion.Item eventKey="6">
                      <Accordion.Header>Product Demo & Website Coming Soon</Accordion.Header>
                      <Accordion.Body>
                          <p>The project demo and final implementation will be updated as development progresses. Check here later!</p>
                      </Accordion.Body>
                  </Accordion.Item>
              </Accordion>
            </div>
            </div></>);
  }
}

import "bootstrap/dist/css/bootstrap.min.css";
import { Button } from "react-bootstrap";
import React, { Component } from 'react';
import {addNewPost} from "./CCscript"
class ContentContainer extends Component {
    state = {
      postColor: "#f59317",
    };
    render() {
      const postColor = this.state.postColor;
      return (
        <div className="content">
          <Button className="float-end m-3" onClick={()=>
            {
              addNewPost(postColor)
              if (postColor==="#fdc959")
              {
                this.setState({postColor:"#f59317"})
              }
              else
              {
                this.setState({postColor:"#fdc959"})
              }
              }}>New post</Button>
          <div className="container mh-100" id="CC">
          </div>
        </div>
      );
    }
  }

export default ContentContainer;
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Nav, Navbar } from "react-bootstrap";
import ContentContainer from "./CC";
import Navigation from "./Navigation";

function App() {
  return (
    <div className="bigdiv">
      <Navigation></Navigation>
      <ContentContainer/>
    </div>
    );

}

export default App;

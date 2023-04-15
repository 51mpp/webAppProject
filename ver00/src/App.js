import "bootstrap/dist/css/bootstrap.min.css";
import { Nav, Navbar } from "react-bootstrap";
import ContentContainer from "./CC";
import Navigation from "./Navigation";
import "./App.css";

function App() {
  return (
    <div className="bigdiv">
          <Navigation></Navigation>

          <ContentContainer />

          <div className="d-flex justify-content-center bgcl pb-3">
              <h1>About us</h1>
          </div>

          <footer>
              <p>
                  ฝากเพื่อนซื้อหน่อย
                  วิชา WEB APPLICATION DEVELOPMENT PROJECT รหัสวิชา
                  01076120 สถาบันเทคโนโลยีพระจอมเกล้าเจ้าคุณทหารลาดกระบัง
              </p>
          </footer>
     </div>

    );

}

export default App;

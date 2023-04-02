import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Nav, Navbar, Button } from "react-bootstrap";
function App() {
  return (
    <div className="bigdiv">
      <div className="nvbdiv">
        <Nav className="justify-content-around navbar-dark shadow-5-strong">
          <Navbar.Brand>Logo</Navbar.Brand>
          <Navbar.Brand>Home</Navbar.Brand>
          <Navbar.Brand>About us</Navbar.Brand>
          <Navbar.Brand>someting</Navbar.Brand>
        </Nav>
      </div>
      <div className="content">
        <Button className="float-end m-3">New post</Button>
      </div>
      <div className="d-flex justify-content-center bgcl">
        <h1>About us</h1>
      </div>
      <footer>
        ฝากเพื่อนซื้อหน่อย วิชาWEB APPLICATION DEVELOPMENT PROJECT รหัสวิชา 01076120 สถาบันเทคโนโลยีพระจอมเกล้าเจ้าคุณทหารลาดกระบัง
      </footer>
    </div>
  );
}

export default App;

<<<<<<< Updated upstream
import logo from './logo.svg';
import './App.css';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
=======
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Nav, Navbar } from "react-bootstrap";
import ContentContainer from "./CC";
function App() {
  return (
    <div className="bigdiv container">
      <div className="nvbdiv row">
        <Nav className="justify-content-around navbar-dark shadow-5-strong">
          <Navbar.Brand>Logo</Navbar.Brand>
          <Navbar.Brand>Home</Navbar.Brand>
          <Navbar.Brand>About us</Navbar.Brand>
          <Navbar.Brand>someting</Navbar.Brand>
        </Nav>
      </div>
      <ContentContainer/>
      <div className="d-flex justify-content-center bgcl row">
        <h1>About us</h1>
      </div>
      <footer className="row">
        ฝากเพื่อนซื้อหน่อย วิชาWEB APPLICATION DEVELOPMENT PROJECT รหัสวิชา 01076120 สถาบันเทคโนโลยีพระจอมเกล้าเจ้าคุณทหารลาดกระบัง
      </footer>
>>>>>>> Stashed changes
    </div>
  );
}

export default App;

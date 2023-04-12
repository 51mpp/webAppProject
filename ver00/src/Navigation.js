/*import "bootstrap/dist/css/bootstrap.min.css";
import { Nav, Navbar } from "react-bootstrap";
function Navigation() {
  return (
    <div className="nvbdiv row">
      <Nav className="justify-content-around navbar-dark shadow-5-strong">
        <Navbar.Brand>Logo</Navbar.Brand>
        <Navbar.Brand>Home</Navbar.Brand>
        <Navbar.Brand>About us</Navbar.Brand>
        <Navbar.Brand>someting</Navbar.Brand>
      </Nav>
    </div>
  );
}

export default Navigation;
*/
import "bootstrap/dist/css/bootstrap.min.css";
import { Nav, Navbar, Container } from "react-bootstrap";
import "./Navigation.css";
function Navigation() {
  return (
    <div>
      <style>.banner</style>
      <Navbar
        collapseOnSelect
        fixed="top"
        expand="lg"
        className="p-md-3 navbar-dark"
      >
        <Container>
          <Navbar.Toggle aria-controls="responsive-navbar-nav"></Navbar.Toggle>
          <Navbar.Brand>Web Zone</Navbar.Brand>
          <Navbar.Collapse id="responsive-navbar-nav">
            <div className="mx-auto"></div>
            <Nav>
              <Nav.Link href="/">Logo</Nav.Link>
              <Nav.Link href="/">Home</Nav.Link>
              <Nav.Link href="/">About us</Nav.Link>
              <Nav.Link href="/">Somethign</Nav.Link>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
      <div className="banner-image w-100 vh-100 d-flex justify-content-center align-items-center">
        <div className="centent text-center">
          <h1 className="text-white">WEB ZONE</h1>
        </div>
      </div>
    </div>
  );
}

export default Navigation;

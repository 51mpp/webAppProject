import { useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Nav, Navbar, Container } from "react-bootstrap";
import "./Navigation.css";


function Navigation() {
    const [color, setColor] = useState(false);
    const chageColor = () => {
        if (window.scrollY >= 550) {
            setColor(true);
        } else {
            setColor(false);
        }
    };
    window.addEventListener("scroll", chageColor);

  return (
    <div>
      <Navbar
              collapseOnSelect
              fixed="top"
              expand="lg"
              id="FatherNav"
              className={color ? "p-md-3 navbar-dark nav2" :"p-md-3 navbar-dark nav1"}
      >
        <Container>
          <Navbar.Toggle aria-controls="responsive-navbar-nav"></Navbar.Toggle>
          <Navbar.Brand>Web Zone</Navbar.Brand>
          <Navbar.Collapse id="responsive-navbar-nav">
            <div className="mx-auto"></div>
            <Nav>
                          <Nav.Link className={color ? "navl2" : "navl1"} href="/"><span>HOME</span></Nav.Link>
                          <Nav.Link className={color ? "navl2" : "navl1"} href="/"><span>ABOUT US</span></Nav.Link>
                          <Nav.Link className={color ? "navl2" : "navl1"}  href="/"><span>MANUAL</span></Nav.Link>
                          <Nav.Link className={color ? "navl2" : "navl1"}  href="/"><span>USER </span></Nav.Link>
            </Nav>
          </Navbar.Collapse>
        </Container>
          </Navbar>

      <div className="banner-image w-100 vh-100 d-flex justify-content-center align-items-center">
        <div className="centent text-center">
             <h1 id="WC" className="">WELCOME TO OUR PROJECT</h1>
            <h1 className="text-white">WEB ZONE</h1>
        </div>
      </div>
      </div>

    );
}

export default Navigation;

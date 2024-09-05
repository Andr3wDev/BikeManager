import React from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';

const NavbarContainer = styled.nav`
  display: flex;
  justify-content: space-between;
  background-color: #333;
  padding: 10px;
`;

const NavLink = styled(Link)`
  color: white;
  text-decoration: none;
  padding: 0 15px;

  &:hover {
    text-decoration: underline;
  }
`;

export const Navbar: React.FC = () => (
  <NavbarContainer>
    <NavLink to="/">Home</NavLink>
    <NavLink to="/bikes">Bikes</NavLink>
  </NavbarContainer>
);

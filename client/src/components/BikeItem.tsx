import React from 'react';
import { useNavigate } from 'react-router-dom';
import { BikeDto } from '../dto/bikeDto';
import { useBikeContext } from '../context/BikeContext';
import styled from 'styled-components';

interface BikeItemProps {
  bike: BikeDto;
}

const StyledItem = styled.li`
  border: 1px solid #ddd;
  margin-bottom: 10px;
  padding: 10px;
  list-style-type: none;
`;

const StyledButton = styled.button`
  margin-right: 5px;
  padding: 5px 10px;
  cursor: pointer;
`;

export const BikeItem: React.FC<BikeItemProps> = ({ bike }) => {
  const { deleteBike } = useBikeContext();
  const navigate = useNavigate();

  const handleDelete = () => {
    if (window.confirm('Are you sure you want to delete this bike?')) {
      deleteBike(bike.id);
    }
  };

  return (
    <StyledItem>
      <div>{bike.brand} {bike.model} ({bike.year})</div>
      <div>Owner: {bike.ownerEmail}</div>
      <StyledButton onClick={() => navigate(`/edit-bike/${bike.id}`)}>Edit</StyledButton>
      <StyledButton onClick={handleDelete}>Delete</StyledButton>
    </StyledItem>
  );
};
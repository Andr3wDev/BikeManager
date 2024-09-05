import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useBikeContext } from '../context/BikeContext';
import { BikeItem } from './BikeItem';
import styled from 'styled-components';

const PageContainer = styled.div`
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
`;

const BikeList = styled.ul`
  padding: 0;
`;

const AddButton = styled.button`
  padding: 10px 20px;
  background-color: #4CAF50;
  color: white;
  border: none;
  cursor: pointer;
  font-size: 16px;
  margin-bottom: 20px;
`;

export const BikeListPage: React.FC = () => {
  const { bikes, fetchBikes } = useBikeContext();
  const navigate = useNavigate();

  useEffect(() => {
    fetchBikes();
  }, [fetchBikes]);

  return (
    <PageContainer>
      <h1>Bike List</h1>
      <AddButton onClick={() => navigate('/add-bike')}>Add New Bike</AddButton>
      <BikeList>
        {bikes.map((bike) => (
          <BikeItem key={bike.id} bike={bike} />
        ))}
      </BikeList>
    </PageContainer>
  );
};
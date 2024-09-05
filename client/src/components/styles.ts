// styles.ts
import styled from 'styled-components';
import { Form as FormikForm, Field } from 'formik';

// Styled form container
export const StyledForm = styled(FormikForm)`
  display: flex;
  flex-direction: column;
  max-width: 300px;
  margin: 0 auto;
`;

// Styled input field
export const StyledInput = styled(Field)`
  margin-bottom: 10px;
  padding: 10px;
  width: 100%; /* Full width */
  box-sizing: border-box; /* Include padding and border in the element's width */
`;

// Styled button
export const StyledButton = styled.button`
  padding: 10px;
  background-color: #4CAF50; /* Green color */
  color: white;
  border: none;
  cursor: pointer;
  border-radius: 4px; /* Optional: for rounded corners */
  font-size: 16px; /* Optional: adjust the font size */
  text-align: center;
  display: inline-block; /* Ensure proper alignment */
  transition: background-color 0.3s ease;

  &:hover {
    background-color: #45a049; /* Slightly darker green on hover */
  }
`;

// Styled error text
export const ErrorText = styled.div`
  color: red;
  margin-bottom: 10px;
`;

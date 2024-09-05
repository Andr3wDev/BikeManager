import React from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { BikeCreateDto } from '../dto/bikeDto';
import { BikeCreateSchema } from '../validations/bikeValidation';
import { StyledForm, StyledInput, StyledButton, ErrorText } from './styles';

interface BikeAddFormProps {
  initialValues: BikeCreateDto;
  onSubmit: (values: BikeCreateDto) => void;
}

export const BikeAddForm: React.FC<BikeAddFormProps> = ({ initialValues, onSubmit }) => {
  return (
    <Formik
      initialValues={initialValues}
      validationSchema={BikeCreateSchema}
      onSubmit={values => onSubmit(values)}
    >
      {() => (
        <StyledForm>
          <div>
            <StyledInput as={Field} name="brand" placeholder="Brand" />
            <ErrorMessage name="brand" component={ErrorText} />
          </div>
          <div>
            <StyledInput as={Field} name="model" placeholder="Model" />
            <ErrorMessage name="model" component={ErrorText} />
          </div>
          <div>
            <StyledInput as={Field} name="year" placeholder="Year" type="number" />
            <ErrorMessage name="year" component={ErrorText} />
          </div>
          <div>
            <StyledInput as={Field} name="ownerEmail" placeholder="Owner Email" />
            <ErrorMessage name="ownerEmail" component={ErrorText} />
          </div>
          <StyledButton type="submit">Add Bike</StyledButton>
        </StyledForm>
      )}
    </Formik>
  );
};

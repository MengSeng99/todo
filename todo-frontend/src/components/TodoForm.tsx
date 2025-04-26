import React, { useState } from 'react';
import { Box, TextField, Button, Paper } from '@mui/material';
import { todoService } from '../services/TodoService';

interface TodoFormProps {
    onAdd: () => void;
}

export const TodoForm: React.FC<TodoFormProps> = ({ onAdd }) => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        
        if (!title.trim()) {
            alert('Please enter a title');
            return;
        }

        try {
            await todoService.create({
                title: title.trim(),
                description: description.trim(),
                isCompleted: false
            });
            
            setTitle('');
            setDescription('');
            onAdd();
        } catch (error) {
            console.error('Failed to add todo:', error);
            alert('Failed to add todo, please try again');
        }
    };

    return (
        <Paper elevation={3} sx={{ p: 3, mb: 3 }}>
            <Box component="form" onSubmit={handleSubmit}>
                <TextField
                    fullWidth
                    label="Title"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    margin="normal"
                    required
                />
                <TextField
                    fullWidth
                    label="Description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    margin="normal"
                    multiline
                    rows={3}
                />
                <Button
                    type="submit"
                    variant="contained"
                    color="primary"
                    sx={{ mt: 2 }}
                >
                    Add Todo
                </Button>
            </Box>
        </Paper>
    );
}; 
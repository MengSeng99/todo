import React, { useState, useEffect } from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, TextField, Button } from '@mui/material';
import { Todo } from '../types/Todo';

interface EditTodoFormProps {
    todo: Todo;
    open: boolean;
    onClose: () => void;
    onSave: (todo: Todo) => void;
}

export const EditTodoForm: React.FC<EditTodoFormProps> = ({ todo, open, onClose, onSave }) => {
    const [title, setTitle] = useState(todo.title);
    const [description, setDescription] = useState(todo.description || '');

    useEffect(() => {
        setTitle(todo.title);
        setDescription(todo.description || '');
    }, [todo]);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onSave({
            ...todo,
            title,
            description
        });
    };

    return (
        <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
            <form onSubmit={handleSubmit}>
                <DialogTitle>Edit Todo</DialogTitle>
                <DialogContent>
                    <TextField
                        autoFocus
                        margin="dense"
                        label="Title"
                        type="text"
                        fullWidth
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        required
                    />
                    <TextField
                        margin="dense"
                        label="Description"
                        type="text"
                        fullWidth
                        multiline
                        rows={4}
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={onClose}>Cancel</Button>
                    <Button type="submit" variant="contained" color="primary">
                        Save
                    </Button>
                </DialogActions>
            </form>
        </Dialog>
    );
}; 
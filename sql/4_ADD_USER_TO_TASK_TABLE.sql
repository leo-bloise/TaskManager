ALTER TABLE "tasks" 
ADD COLUMN "user_id" INTEGER REFERENCES users("id");
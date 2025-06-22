ALTER TABLE "categories" DROP CONSTRAINT "categories_name_key";
ALTER TABLE "categories" ADD CONSTRAINT  categories_name_user_id_unique UNIQUE("name", "user_id");